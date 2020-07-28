using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTitle : ScreenObject
{
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button rulesButton = null;
    [SerializeField] private Button newGameButton = null;

    #region events
    public event Action OnRulesClicked;
    public event Action OnNewGameClicked;
    #endregion

    public override void Appear()
    {
        base.Appear();

        rulesButton.onClick.AddListener(OnClickedRules);
        newGameButton.onClick.AddListener(OnClickedNewGame);
    }

    public override void EndAppear()
    {
        base.EndAppear();
    }

    public override void Disappear()
    {
        base.Disappear();

        rulesButton.onClick.RemoveListener(OnClickedRules); 
        newGameButton.onClick.RemoveListener(OnClickedNewGame);
    }

    private void OnClickedRules()
    {
        Disappear();

        OnRulesClicked?.Invoke();
    }

    private void OnClickedNewGame()
    {
        OnNewGameClicked?.Invoke();
    }
}
