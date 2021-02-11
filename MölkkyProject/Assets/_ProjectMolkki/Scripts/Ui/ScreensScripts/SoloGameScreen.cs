using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloGameScreen : ScreenObject
{
    [Header("BannerInfos")]
    [SerializeField] private GameObject playerInfoBanner = null;
    [SerializeField] private Transform topBanner = null;
    [Space]
    [Header("BannerInfoMaths")]
    [SerializeField] private float spacement = 50f;

    private List<PlayerInfoBanner> playerInfoBannerList = new List<PlayerInfoBanner>();

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
    }

    private void RepositionateTopBannerInfo(int index, PlayerInfoBanner playerInfosBanner)
    {
        playerInfosBanner.Rect.localPosition = playerInfosBanner.Rect.localPosition + new Vector3((playerInfosBanner.Rect.rect.width * index) + (spacement * index),0,0);
    }

    private void ComputeScroolZone()
    {
        RectTransform lScroolZone = topBanner.transform as RectTransform;
        RectTransform lLastPlayerInfoBanner = playerInfoBannerList[playerInfoBannerList.Count - 1].Rect;

        if(lLastPlayerInfoBanner.localPosition.x + lLastPlayerInfoBanner.rect.xMax >= lScroolZone.rect.xMax)
        {
            Rect lRectTemp = lScroolZone.rect;
            lRectTemp.xMax = lLastPlayerInfoBanner.localPosition.x + lLastPlayerInfoBanner.rect.xMax;
        }
    }
}
