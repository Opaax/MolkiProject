using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private int currentPointSelected = default;

    private PlayerInfoBanner currentPlayer = default;

    private List<PlayerInfoBanner> playerInfoBannerList = new List<PlayerInfoBanner>();
    private List<ButtonPoint> buttonsPoint = new List<ButtonPoint>();

    public event Action onQuitClicked = default;
    public event Action onMissedClicked = default;
    public event Action<int> onPointClicked = default;

    public void InitPlayer(List<PlayerInfo> playerList)
    {
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            GameObject lPlayerInfoBannerObject = Instantiate(playerInfoBanner, topBanner);
            PlayerInfoBanner lPlayerInfoBanner = lPlayerInfoBannerObject.GetComponent<PlayerInfoBanner>();

            lPlayerInfoBanner.InitBannerInfo(playerList[i].Pseudo, 0.ToString());

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

        currentPlayer = playerInfoBannerList[0];
    }

    public void InitButtons()
    {
        quitbutton.onClick.AddListener(OnQuitClicked);
        missedButton.onClick.AddListener(OnMissedClicked);

        GetAllEventButtonPoint();
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

        buttonsPoint.Clear();
    }
    #endregion
    #region ButtonEvents
    private void ButtonPoint_OnClicked(int pointClicked)
    {
        pointButton.onClick.RemoveAllListeners();

        currentPointSelected = pointClicked;

        pointButton.onClick.AddListener(OnPointClicked);
    }

    private void OnPointClicked()
    {
        onPointClicked?.Invoke(currentPointSelected);
    }

    private void OnMissedClicked()
    {
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
