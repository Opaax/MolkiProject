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
    
    private float baseSizeScroolZone = default;
    private float distanceBetweenLastInfoAddButton = default;  
    private float distanceBetweenInfo = default;  

    public override void EndAppear()
    {
        base.EndAppear();

        addPlayerButton.onClick.AddListener(AddPlayer);

        ComputeDistanceBetweenAddButtonLastPlayerInfo();
        ComputeBaseSizeScroolZone();
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

        ComputeScrollZone();
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

            this.Log("FinalSize " + lFinalSize);
            this.Log("Prev " + lPrevSize);
            this.Log("Compute " + (lFinalSize - lPrevSize));

            lScroolZone.sizeDelta = new Vector2(lScroolZone.sizeDelta.x, lFinalSize);
            lScroolZone.DOAnchorPosY(lScroolZone.anchoredPosition.y + (lFinalSize - lPrevSize), 0.3f).OnComplete(() =>
            {
                Debug.Log("Complete");
            });
        }
    }
}
