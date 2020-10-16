using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoloScreen : ScreenObject
{
    [Header("PlayerList")]
    [SerializeField] private List<PlayerInfo> playerInfoList = new List<PlayerInfo>();
    [Space]
    [Header("AddPlayer")]
    [SerializeField] private Button addPlayerButton = null;
    [SerializeField] private GameObject playerInfo = null;
    [SerializeField] private Transform parentContener = null;
    [Space]
    [Header("buttons")]
    [SerializeField] private Button playButton = null;
    
    private float baseSizeScroolZone = default;
    private float distanceBetweenLastInfoAddButton = default;  
    private float distanceBetweenInfo = default;

    public event Action<PlayerInfo> OnPlayerInfoClicked = default;
    public event Action OnCantRemove = default;
    public event Action OnRemove = default;
    public event Action<bool> OnPlay = default;

    public override void EndAppear()
    {
        base.EndAppear();

        GetEventPlayerInfo();
        AddButtonEvent();

        ComputeDistanceBetweenAddButtonLastPlayerInfo();
        ComputeBaseSizeScroolZone();
    }

    private void AddButtonEvent ()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        addPlayerButton.onClick.AddListener(AddPlayer);
    }

    private void RemoveButtonEvent ()
    {
        playButton.onClick.RemoveListener(OnPlayClicked);
        addPlayerButton.onClick.RemoveListener(AddPlayer);
    }

    private void OnPlayClicked()
    {
        RemoveButtonEvent();
        RemoveAllEventPlayerInfo();

        if (AllPlayerCheck())
        {
            OnPlay.Invoke(true);
        }
        else
        {
            OnPlay.Invoke(false);
        }
    }

    private bool AllPlayerCheck()
    {
        for (int i = playerInfoList.Count - 1; i >= 0; i--)
        {
            if (playerInfoList[i].IsReady) continue;
            else
            {
                return false;
            }
        }

        return true;
    }

    public void ResetEventButton()
    {
        AddButtonEvent();
    }

    public void ResetEventPlayerInfo()
    {
        GetEventPlayerInfo();
    }

    private void GetEventPlayerInfo()
    {
        for (int i = playerInfoList.Count - 1; i >= 0; i--)
        {
            playerInfoList[i].onPlayerInfoClicked += PlayerInfo_OnClicked;
        }
    }

    private void RemoveAllEventPlayerInfo()
    {
        for (int i = playerInfoList.Count - 1; i >= 0; i--)
        {
            playerInfoList[i].onPlayerInfoClicked -= PlayerInfo_OnClicked;
        }
    }

    private void PlayerInfo_OnClicked(PlayerInfo clickedPlayer)
    {
        OnPlayerInfoClicked?.Invoke(clickedPlayer);

        RemoveButtonEvent();
        RemoveAllEventPlayerInfo();
    }

    private void ComputeDistanceBetweenAddButtonLastPlayerInfo()
    {
        RectTransform lButtonParent = addPlayerButton.transform.parent as RectTransform;
        RectTransform lLastPlayerInfo = playerInfoList[playerInfoList.Count - 1].Rect;
        RectTransform lFirstPlayerInfo = playerInfoList[0].Rect;

        distanceBetweenLastInfoAddButton = lButtonParent.position.y - lLastPlayerInfo.position.y;
        distanceBetweenInfo = lFirstPlayerInfo.position.y - lLastPlayerInfo.position.y;
    }

    private void ComputeBaseSizeScroolZone()
    {
        RectTransform lRectTransform = (RectTransform)parentContener.transform;

        baseSizeScroolZone = lRectTransform.sizeDelta.y;
    }

    private void AddPlayer()
    {
        GameObject lLastInfoObject = Instantiate(playerInfo,parentContener);

        PlayerInfo lNewPlayerInfo = lLastInfoObject.GetComponent<PlayerInfo>();

        RectTransform lLastPlayerInfo = playerInfoList[playerInfoList.Count - 1].Rect;
        RectTransform lButtonParent = addPlayerButton.transform.parent as RectTransform;

        lNewPlayerInfo.Rect.position = lLastPlayerInfo.position - new Vector3(0, distanceBetweenInfo, 0);
        lButtonParent.position = lNewPlayerInfo.Rect.position + new Vector3(0, distanceBetweenLastInfoAddButton, 0);

        playerInfoList.Add(lNewPlayerInfo);

        lNewPlayerInfo.onPlayerInfoClicked += PlayerInfo_OnClicked;

        ComputeScrollZone();
    }

    public void RemovePlayer(PlayerInfo playerInfoToRemove)
    {
        int lIndexToRemove = 0;

        if(CheckRemovingPlayer())
        {
            playerInfoToRemove.onPlayerInfoClicked -= PlayerInfo_OnClicked;

            lIndexToRemove = playerInfoList.IndexOf(playerInfoToRemove);

            playerInfoList.RemoveAt(lIndexToRemove);

            Destroy(playerInfoToRemove.gameObject);

            ReplaceAllPlayerInfo(lIndexToRemove);
            ReplaceButtonAdd();
            ComputeScrollZone();

            OnRemove?.Invoke();
        }
        else
        {
            OnCantRemove?.Invoke();
        }
    }

    private void ReplaceButtonAdd()
    {
        RectTransform lButtonParent = addPlayerButton.transform.parent as RectTransform;

        lButtonParent.position = playerInfoList[playerInfoList.Count-1].Rect.position + new Vector3(0, distanceBetweenLastInfoAddButton, 0);
    }

    private void ReplaceAllPlayerInfo(int fromIndexToReplace)
    {
        for (int i = playerInfoList.Count - 1; i >= 0; i--)
        {
            if(i >= fromIndexToReplace)
            {
                playerInfoList[i].Rect.position = playerInfoList[i].Rect.position + new Vector3(0, distanceBetweenInfo, 0);
            }
        }
    }

    private bool CheckRemovingPlayer()
    {
        return playerInfoList.Count > 2;
    }

    private void ComputeScrollZone ()
    {
        RectTransform lButtonParent = addPlayerButton.transform.parent as RectTransform;
        RectTransform lScroolZone = parentContener.transform as RectTransform;

        float lYAddButton = lButtonParent.localPosition.y * -1;

        if (lYAddButton > lScroolZone.sizeDelta.y)
        {
            float lFinalSize = lYAddButton + 80 + 20;
            float lPrevSize = lScroolZone.sizeDelta.y;

            lScroolZone.sizeDelta = new Vector2(lScroolZone.sizeDelta.x, lFinalSize);
            lScroolZone.DOAnchorPosY(lScroolZone.anchoredPosition.y + (lFinalSize - lPrevSize), 0.3f).OnComplete(() =>
            {
                
            });
        }
        else
        {
            float lFinalSize = lYAddButton + 80;
            float lPrevSize = lScroolZone.sizeDelta.y;

            if (lFinalSize < baseSizeScroolZone)
            {
                lScroolZone.sizeDelta = new Vector2(lScroolZone.sizeDelta.x, baseSizeScroolZone);
                return;
            }

            lScroolZone.sizeDelta = new Vector2(lScroolZone.sizeDelta.x, lFinalSize);
            lScroolZone.DOAnchorPosY(lScroolZone.anchoredPosition.y + (lFinalSize - lPrevSize), 0.3f).OnComplete(() =>
            {

            });
        }
    }
}
