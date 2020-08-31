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
    [SerializeField] SoloScreen soloScreen = null;

    private ScreenManager screenManager = new ScreenManager();

    private void Start()
    {
        screenManager.isAllScreensInit += ScreenManager_AllScreenInit;

        screenManager.Init();
    }

    #region AddScreens
    private void AddMainTitle()
    {
        mainTitle.Appear();

        screenManager.AddActifScreen(mainTitle);

        mainTitle.OnDisappearEnd += MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked += MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked += MainTitle_OnNewGame;
    }

    private void AddRulesScreen()
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
        popUpNewGame.OnSoloClicked += NewGamePopUp_SoloGame;
        popUpNewGame.OnTeamClicked += NewGamePopUp_TeamGame;
    }

    private void AddSoloScreenGame ()
    {
        soloScreen.Appear();

        screenManager.AddActifScreen(soloScreen);

    }

    #endregion

    #region Main Title Methodes
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

    #endregion

    #region New Game Pop up Methodes
    private void NewGamePopUp_TeamGame()
    {
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;
        popUpNewGame.OnSoloClicked -= NewGamePopUp_SoloGame;
        popUpNewGame.OnTeamClicked -= NewGamePopUp_TeamGame;
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;

        screenManager.RemoveInactifScreen(popUpNewGame);
        screenManager.RemoveInactifScreen(mainTitle);

        mainTitle.Disappear();
    }

    private void NewGamePopUp_SoloGame()
    {
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;
        popUpNewGame.OnSoloClicked -= NewGamePopUp_SoloGame;
        popUpNewGame.OnTeamClicked -= NewGamePopUp_TeamGame;
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;

        screenManager.RemoveInactifScreen(popUpNewGame);
        screenManager.RemoveInactifScreen(mainTitle);

        mainTitle.Disappear();

        AddSoloScreenGame();
    }

    private void NewGamePopUp_OnClose()
    {
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;

        screenManager.RemoveInactifScreen(popUpNewGame);

        mainTitle.OnDisappearEnd += MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked += MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked += MainTitle_OnNewGame;
    }

    #endregion

    #region Screen Manager Methodes
    private void ScreenManager_AllScreenInit()
    {
        screenManager.isAllScreensInit -= ScreenManager_AllScreenInit;

        AddMainTitle();
    }

    #endregion

    #region RulesScreeen Methodes
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

    #endregion

    private void OnDestroy()
    {
        mainTitle.OnDisappearEnd -= MainTitle_OnDisappearEnd;
        mainTitle.OnRulesClicked -= MainTitle_OnClickedRules;
        mainTitle.OnNewGameClicked -= MainTitle_OnNewGame;

        rulesScreen.OnBack -= RulesScreen_OnBack;
        rulesScreen.OnDisappearEnd -= RulesScreen_OnDisappearEnd;

        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;
        popUpNewGame.OnSoloClicked -= NewGamePopUp_SoloGame;
        popUpNewGame.OnTeamClicked -= NewGamePopUp_TeamGame;
        popUpNewGame.OnClosePopUp -= NewGamePopUp_OnClose;
    }
}
