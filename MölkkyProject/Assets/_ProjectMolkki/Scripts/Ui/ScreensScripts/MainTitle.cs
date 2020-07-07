﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTitle : ScreenObject
{
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button rulesButton = null;

    #region events
    public event Action OnRulesClicked;
    #endregion

    public override void Appear()
    {
        base.Appear();

        rulesButton.onClick.AddListener(OnClickedRules);
    }

    public override void EndAppear()
    {
        base.EndAppear();
    }

    public override void Disappear()
    {
        base.Disappear();

        rulesButton.onClick.RemoveListener(OnClickedRules);
    }

    private void OnClickedRules()
    {
        Disappear();

        OnRulesClicked?.Invoke();
    }
}
