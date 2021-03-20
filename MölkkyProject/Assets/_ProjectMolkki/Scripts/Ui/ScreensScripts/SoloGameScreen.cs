using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoloGameScreen : ScreenObject
{
    [Header("BannerInfos")]
    [SerializeField] private GameObject playerInfoBanner = null;
    [SerializeField] private Transform topBanner = null;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button quitbutton = null;
    [SerializeField] private Button missedButton = null;
    [SerializeField] private Button pointButton = null;
    [Space]
    [Header("BannerInfoMaths")]
    [SerializeField] private float spacement = 50f;
    [SerializeField] private float delaySpawn = .4f;
    [Header("Tweens")]
    [Header("Point Selected")]
    [SerializeField] private float timeToUnScale = .1f;
    [SerializeField] private Ease easeUnScale = Ease.InOutSine;
    [SerializeField] private float timeToScaleUp = .3f;
    [SerializeField] private Ease easeScaleUP = Ease.InOutSine;
    [SerializeField] private float delayBetweenScales = .4f;

    private int _currentPointSelected = default;
    private int currentIndexPlayer = 0;
    private int pointToWin = 0;
    private int maxMiss = 0;

    private PlayerInfoBanner _currentPlayer = default;

    private List<PlayerInfoBanner> playerInfoBannerList = new List<PlayerInfoBanner>();
    private List<ButtonPoint> buttonsPoint = new List<ButtonPoint>();


    public event Action onQuitClicked = default;
    public event Action onMissedClicked = default;
    public event Action<PlayerInfo> onPlayerWin = default;
    public event Action<PlayerInfo> onPlayerOverPoint = default;
    public event Action<int> onPointClicked = default;
    public PlayerInfoBanner CurrentPlayer { get => _currentPlayer; }
    public int CurrentPointSelected { get => _currentPointSelected; }


    public void InitPlayers(List<PlayerInfo> playerList, int maxPointToWin, int maxMissToLose)
    {
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            GameObject lPlayerInfoBannerObject = Instantiate(playerInfoBanner, topBanner);
            PlayerInfoBanner lPlayerInfoBanner = lPlayerInfoBannerObject.GetComponent<PlayerInfoBanner>();

            lPlayerInfoBanner.InitBannerInfo(playerList[i]);

            playerInfoBannerList.Add(lPlayerInfoBanner);
        }

        Shuffle.ShuffleList(playerInfoBannerList);

        for (int i = playerInfoBannerList.Count - 1; i >= 0; i--)
        {
            PlayerInfoBanner lPlayerInfoBanner = playerInfoBannerList[i];

            RepositionateTopBannerInfo(i, lPlayerInfoBanner);
        }

        ComputeScroolZone();

        for (int i = 0; i < playerInfoBannerList.Count; i++)
        {
            playerInfoBannerList[i].SpawnBannerInfo(delaySpawn * i);
        }

        currentIndexPlayer = 0;

        pointToWin = maxPointToWin;
        maxMiss = maxMissToLose;

        _currentPlayer = playerInfoBannerList[currentIndexPlayer];
    }

    public void InitButtons()
    {
        quitbutton.onClick.AddListener(OnQuitClicked);
        missedButton.onClick.AddListener(OnMissedClicked);

        GetAllEventButtonPoint();
    }

    public void UpdateCurrentPlayer()
    {
        _currentPlayer.UpdateInfos();

        if (_currentPlayer.Player.Score == pointToWin)
        {
            onPlayerWin?.Invoke(_currentPlayer.Player);

            return;
        }
        else if(_currentPlayer.Player.Score > pointToWin)
        {
            onPlayerOverPoint?.Invoke(_currentPlayer.Player);

            return;
        }

        if (_currentPlayer.Player.Missed == maxMiss)
        {

        }

        NextPlayer();

        GetAllEventButtonPoint();
    }

    public void NextPlayer()
    {
        if (currentIndexPlayer >= playerInfoBannerList.Count -1) currentIndexPlayer = 0;
        else currentIndexPlayer++;

        _currentPlayer = playerInfoBannerList[currentIndexPlayer];

        _currentPointSelected = 0;
    }

    #region ButtonsPointEvent
    private void GetAllEventButtonPoint()
    {
        buttonsPoint = GetComponentsInChildren<ButtonPoint>().ToList();

        for (int i = buttonsPoint.Count - 1; i >= 0; i--)
        {
            buttonsPoint[i].onPointClicked += ButtonPoint_OnClicked;
        }
    }

    private void RemoveAllEventButtonPoint()
    {
        for (int i = buttonsPoint.Count - 1; i >= 0; i--)
        {
            buttonsPoint[i].onPointClicked -= ButtonPoint_OnClicked;
        }

        //buttonsPoint.Clear();
    }
    #endregion
    #region ButtonEvents
    private void ButtonPoint_OnClicked(int pointClicked)
    {
        pointButton.onClick.RemoveAllListeners();

        _currentPointSelected = pointClicked;

        ModifyButtonFeedBack(_currentPointSelected);

        pointButton.onClick.AddListener(OnPointClicked);
    }

    private void OnPointClicked()
    {
        RemoveAllEventButtonPoint(); 

        onPointClicked?.Invoke(_currentPointSelected);
    }

    private void OnMissedClicked()
    {
        RemoveAllEventButtonPoint();

        onMissedClicked?.Invoke();
    }

    private void OnQuitClicked()
    {
        onQuitClicked?.Invoke();
    }
    #endregion
    #region ComputeScroolZone /// RepositionBannerPlayerInfo
    private void RepositionateTopBannerInfo(int index, PlayerInfoBanner playerInfosBanner)
    {
        playerInfosBanner.Rect.localPosition = playerInfosBanner.Rect.localPosition + new Vector3((playerInfosBanner.Rect.rect.width * index) + (spacement * index),0,0);
    }

    private void ComputeScroolZone()
    {
        RectTransform lScroolZone = topBanner.transform as RectTransform;
        RectTransform lLastPlayerInfoBanner = playerInfoBannerList[playerInfoBannerList.Count - 1].Rect;

        Rect lScroolRect = lScroolZone.rect;

        if(lLastPlayerInfoBanner.localPosition.x + lLastPlayerInfoBanner.rect.xMax >= lScroolZone.rect.xMax)
        {
            lScroolZone.sizeDelta = new Vector2((lLastPlayerInfoBanner.localPosition.x + lLastPlayerInfoBanner.rect.xMax - lScroolZone.rect.xMax) + spacement, lScroolZone.sizeDelta.y);
            lScroolZone.DOAnchorPosX(lScroolZone.rect.xMax, 0.9f).OnComplete(() =>
            {

            });
        }
    }
    #endregion

    private void ModifyButtonFeedBack(int currentPointSelected)
    {
        pointButton.transform.DOScale(Vector3.zero, timeToUnScale).SetEase(easeUnScale);
        pointButton.GetComponentInChildren<TextMeshProUGUI>().text = String.Format("{0} points", currentPointSelected);
        pointButton.transform.DOScale(Vector3.one, timeToScaleUp).SetEase(easeScaleUP).SetAutoKill(true).SetDelay(delayBetweenScales);
    }
    public override void Disappear()
    {
        base.Disappear();

        RemoveAllEventButtonPoint();
    }

    private void OnDestroy()
    {
        RemoveAllEventButtonPoint();
    }
}
