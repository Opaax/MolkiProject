using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpNewGame : PopUpObject
{
    [Space]
    [Header("ButtonsPopUpNewGame")]
    [SerializeField] private Button soloGame = null;
    [SerializeField] private Button teamGame = null;

    #region
    public event Action OnSoloClicked;
    public event Action OnTeamClicked;
    #endregion

    public override void Appear()
    {
        base.Appear();

        soloGame.onClick.AddListener(SoloGameOnClicked);
        teamGame.onClick.AddListener(TeamGameOnClicked);
    }

    private void TeamGameOnClicked()
    {
        OnTeamClicked?.Invoke();

        Disappear();
    }

    private void SoloGameOnClicked()
    {
        OnSoloClicked?.Invoke();

        Disappear();
    }

    public override void Disappear()
    {
        base.Disappear();

        soloGame.onClick.RemoveListener(SoloGameOnClicked);
        teamGame.onClick.RemoveListener(TeamGameOnClicked);
    }
}
