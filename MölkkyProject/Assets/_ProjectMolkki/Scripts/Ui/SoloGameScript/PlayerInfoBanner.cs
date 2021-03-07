using DG.Tweening;
using System;
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
    [Space]
    [Header("Miss settings")]
    [SerializeField] private MissInfoView[] missInfoView = default;

    private PlayerInfo _player = default;

    public RectTransform Rect { get => _rect;}
    public PlayerInfo Player { get => _player;}

    private void Start()
    {
        transform.DOScale(0, 0);
    }

    public void InitBannerInfo(PlayerInfo player)
    {
        nameText.text = player.Pseudo;
        scoreText.text = player.Score.ToString();

        _player = player;
    }

    public void SpawnBannerInfo (float delay)
    {
        transform.DOScale(1, .2f).SetDelay(delay).SetEase(Ease.InBounce);
    }

    public void UpdateInfos()
    {
        for (int i = 0; i < _player.Missed; i++)
        {
            missInfoView[i].AppearImage();
        }

        scoreText.text = _player.Score.ToString();
    }
}

