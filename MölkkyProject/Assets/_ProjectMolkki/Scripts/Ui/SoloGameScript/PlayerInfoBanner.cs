using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoBanner : MonoBehaviour
{
    [Header("TextInfos")]
    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [Space]
    [Header("Transform")]
    [SerializeField] private RectTransform _rect = null;

    public RectTransform Rect { get => _rect;}

    public void InitBannerInfo(string nameInfo, string scoreInfo)
    {
        nameText.text = nameInfo;
        scoreText.text = scoreInfo;
    }
}
