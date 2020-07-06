using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] MainTitle mainTitle = null;
    [SerializeField] RulesScreen rulesScreen = null;

    private ScreenManager screenManager = new ScreenManager();

    private void Start()
    {
        screenManager.isAllScreensInit += ScreenManager_AllScreenInit;

        screenManager.Init();
    }

    private void ScreenManager_AllScreenInit()
    {
        screenManager.isAllScreensInit -= ScreenManager_AllScreenInit;

        AddTitleCard();
    }

    private void AddTitleCard()
    {
        mainTitle.Appear();

        mainTitle.OnDisappearEnd += MainTitle_OnDissappearEnd;
        mainTitle.OnRulesClicked += MainTitle_OnClickedRules;
    }

    private void MainTitle_OnClickedRules()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDissappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;

        AddRulesScreen();
    }

    private void MainTitle_OnDissappearEnd(ScreenObject sender)
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDissappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;
    }

    private void AddRulesScreen ()
    {
        rulesScreen.Appear();
    }

    private void OnDestroy()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDissappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;
    }
}
