using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] MainTitle mainTitle = null;
    [SerializeField] RulesScreen rulesScreen = null;
    [SerializeField] PopUpNewGame popUpNewGame = null;

    private ScreenManager screenManager = new ScreenManager();

    private void Start()
    {
        screenManager.isAllScreensInit += ScreenManager_AllScreenInit;

        screenManager.Init();
    }

    private void ScreenManager_AllScreenInit()
    {
        screenManager.isAllScreensInit -= ScreenManager_AllScreenInit;

        AddMainTitle();
    }

    private void AddMainTitle()
    {
        mainTitle.Appear();

        screenManager.AddActifScreen(mainTitle);

        mainTitle.OnDisappearEnd += MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked += MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked += MainTitle_OnNewGame;
    }

    private void MainTitle_OnNewGame()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked -= MainTitle_OnNewGame;

        AddPopUpNewGame();
    }

    private void MainTitle_OnClickedRules()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;

        screenManager.RemoveInactifScreen(mainTitle);

        AddRulesScreen();
    }

    private void MainTitle_OnDisappearEnd(ScreenObject sender)
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;

        screenManager.RemoveInactifScreen(sender);
    }

    private void AddRulesScreen ()
    {
        rulesScreen.Appear();

        screenManager.AddActifScreen(rulesScreen);

        rulesScreen.OnBack += RulesScreen_OnBack;
        rulesScreen.OnDisappearEnd += RulesScreen_OnDisappearEnd;
    }

    private void AddPopUpNewGame()
    {
        popUpNewGame.Appear();

        screenManager.AddActifScreen(popUpNewGame);

        popUpNewGame.OnClosePopUp += NewGamePopUp_OnClose;
    }

    private void NewGamePopUp_OnClose()
    {
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;

        screenManager.RemoveInactifScreen(popUpNewGame);

        mainTitle.OnDisappearEnd += MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked += MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked += MainTitle_OnNewGame;
    }

    private void RulesScreen_OnDisappearEnd(ScreenObject sender)
    {
        rulesScreen.OnBack -= RulesScreen_OnBack;
        rulesScreen.OnDisappearEnd -= RulesScreen_OnDisappearEnd;

        screenManager.RemoveInactifScreen(rulesScreen);
    }

    private void RulesScreen_OnBack()
    {
        rulesScreen.OnBack -= RulesScreen_OnBack;
        rulesScreen.OnDisappearEnd -= RulesScreen_OnDisappearEnd;

        screenManager.RemoveInactifScreen(rulesScreen);

        AddMainTitle();
    }

    private void OnDestroy()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked -= MainTitle_OnNewGame;

        rulesScreen.OnBack -= RulesScreen_OnBack;
        rulesScreen.OnDisappearEnd -= RulesScreen_OnDisappearEnd;
    }
}
