using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoBanner : MonoBehaviour
{
    [Header("TextInfos")]
    [SerializeField] private TextMeshPro nameText = null;
    [SerializeField] private TextMeshPro scoreText = null;

    private void InitBannerInfo(string nameInfo, string scoreInfo)
    {
        nameText.text = nameInfo;
        scoreText.text = scoreInfo;
    }
}
