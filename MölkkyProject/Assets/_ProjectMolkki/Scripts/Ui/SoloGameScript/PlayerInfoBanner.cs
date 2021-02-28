using DG.Tweening;
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
}
