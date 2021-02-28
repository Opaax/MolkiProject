using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("RectTransform")]
    [SerializeField] private RectTransform rect = null;
    [Space]
    [Header("BackgroundTransform")]
    [SerializeField] private RectTransform rectBackground = null;
    [Space]
    [Header("ImageRelativeColors")]
    [SerializeField] private Image imageToChangeColor = null;
    [SerializeField] private Color colorReady = default;
    [SerializeField] private Color colorUnReady = default;
    [SerializeField] private float fadeTimeColor = 0.5f;
    [Space]
    [Header("Interactable")]
    [SerializeField] private Button clickableBackground = null;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI pseudoTextView = null;

    private bool _isReady = false;

    private string _pseudo = default;
    private string _defaultString = "Click Here";

    private int _score = 0;

    public event Action<PlayerInfo> onPlayerInfoClicked = default;

    public RectTransform Rect { get => rect; set => rect = value; }
    public RectTransform RectBackground { get => rectBackground;}
    public string Pseudo { get => _pseudo; set { _pseudo = value; UpdatePseudo(_pseudo);} }
    public string DefaultString { get => _defaultString; set => _defaultString = value; }
    public bool IsReady { get => _isReady; set => _isReady = value; }
    public int Score { get => _score; set => _score = value; }

    private void Start()
    {
        if(clickableBackground)
            clickableBackground.onClick.AddListener(OnClickedBackground);
    }

    private void OnClickedBackground()
    {
        onPlayerInfoClicked?.Invoke(this);
    }

    private void UpdatePseudo(string pseudo)
    {
        pseudoTextView.text = _pseudo = pseudo;        
    }

    public void CheckPseudoColor()
    {
        if (_pseudo == _defaultString || _pseudo == "")
        {

            if (imageToChangeColor.color != colorUnReady)
                imageToChangeColor.DOColor(colorUnReady, fadeTimeColor);

            _isReady = false;
        }
        else
        {
            if (imageToChangeColor.color != colorReady)
                imageToChangeColor.DOColor(colorReady, fadeTimeColor);

            _isReady = true;
        }
    }
}
