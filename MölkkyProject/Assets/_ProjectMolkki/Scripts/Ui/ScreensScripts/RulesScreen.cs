using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesScreen : ScreenObject
{
    [Space]
    [Header("Button")]
    [SerializeField] private Button buttonBack = null;
    [Space]
    [Header("ScrollingArea")]
    [SerializeField] private RectTransform scrollingContent = null;

    #region event
    public event Action OnBack;
    #endregion

    public override void EndDisappear()
    {
        base.EndDisappear();
        scrollingContent.position = new Vector3(scrollingContent.position.x, 0);
    }

    public override void Appear()
    {
        base.Appear();

        buttonBack.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        Disappear();

        buttonBack.onClick.RemoveListener(OnBackClicked);

        OnBack?.Invoke();
    }
}
