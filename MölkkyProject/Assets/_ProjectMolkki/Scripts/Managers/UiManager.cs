using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] MainTitle mainTitle = null;
    [SerializeField] RulesScreen rulesScreen = null;
    [SerializeField] SoloScreen soloScreen = null;
    [Space]
    [Header("PopUp")]
    [SerializeField] PopUpNewGame popUpNewGame = null;
    [SerializeField] PopUpSetPlayerInfo popUpPlayerInfo = null;
    [SerializeField] PopUpAllPlayerNotSet popUpAllPlayerNotSet = null;


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

        soloScreen.OnPlayerInfoClicked += SoloScreen_OnPlayerInfoClicked;
        soloScreen.OnPlay += SoloScreen_OnPlayClicked;
    }

    private void AddPopUpPlayerInfo()
    {
        popUpPlayerInfo.Appear();

        screenManager.AddActifScreen(popUpPlayerInfo);

        popUpPlayerInfo.OnClosePopUp += popUpPlayerInfo_OnClose;
        popUpPlayerInfo.onRemoveClicked += popUpPlayerInfo_OnRemove;
    }

    private void AddPopUpAllPlayerNotSet()
    {
        popUpAllPlayerNotSet.Appear();

        screenManager.AddActifScreen(popUpAllPlayerNotSet);

        popUpAllPlayerNotSet.OnClosePopUp += popUpAllPlayerNotSet_OnClose;
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
        popUpNewGame.OnSoloClicked -= NewGamePopUp_SoloGame;
        popUpNewGame.OnTeamClicked -= NewGamePopUp_TeamGame;

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

    #region SoloScreen Methodes
    private void SoloScreen_OnPlayerInfoClicked(PlayerInfo playerClicked)
    {
        AddPopUpPlayerInfo();
        popUpPlayerInfo.SetPlayerInfo(playerClicked);

        soloScreen.OnPlayerInfoClicked -= SoloScreen_OnPlayerInfoClicked;
    }

    private void SoloScreen_OnPlayClicked(bool isStartGame)
    {
        if (!isStartGame)
        {
            AddPopUpAllPlayerNotSet();
            this.Log("Can't start, all player are set");
        }
        else
        {
            this.Log("Let's start");

        }
    }

    private void SoloScreen_OnCantRemovingPlayer()
    {
        soloScreen.OnRemove -= SoloScreen_OnRemovingPlayer;
        soloScreen.OnCantRemove -= SoloScreen_OnCantRemovingPlayer;

        this.Log("player can't be remove, you need at least two player");
    }

    private void SoloScreen_OnRemovingPlayer()
    {
        soloScreen.OnRemove -= SoloScreen_OnRemovingPlayer;
        soloScreen.OnCantRemove -= SoloScreen_OnCantRemovingPlayer;

        this.Log("player is removed");
    }
    #endregion

    #region popUpPlayerInfo Methodes
    private void popUpPlayerInfo_OnClose()
    {
        popUpPlayerInfo.OnClosePopUp -= popUpPlayerInfo_OnClose;
        popUpPlayerInfo.onRemoveClicked -= popUpPlayerInfo_OnRemove;

        screenManager.RemoveInactifScreen(popUpPlayerInfo);

        soloScreen.OnPlayerInfoClicked += SoloScreen_OnPlayerInfoClicked;
        soloScreen.ResetEventButton();
        soloScreen.ResetEventPlayerInfo();
    }

    private void popUpPlayerInfo_OnRemove(PlayerInfo playerInfoToRemove)
    {
        popUpPlayerInfo.onRemoveClicked -= popUpPlayerInfo_OnRemove;

        soloScreen.OnRemove += SoloScreen_OnRemovingPlayer;
        soloScreen.OnCantRemove += SoloScreen_OnCantRemovingPlayer;

        soloScreen.RemovePlayer(playerInfoToRemove);
    }

    #endregion

    #region popUpAllPlayerNotSet
    private void popUpAllPlayerNotSet_OnClose()
    {
        popUpAllPlayerNotSet.OnClosePopUp -= popUpAllPlayerNotSet_OnClose;

        screenManager.RemoveInactifScreen(popUpAllPlayerNotSet);

        soloScreen.ResetEventButton();
        soloScreen.ResetEventPlayerInfo();
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

        popUpPlayerInfo.OnClosePopUp -= popUpPlayerInfo_OnClose;
        popUpPlayerInfo.onRemoveClicked -= popUpPlayerInfo_OnRemove;

        soloScreen.OnPlayerInfoClicked -= SoloScreen_OnPlayerInfoClicked;
        soloScreen.OnPlay -= SoloScreen_OnPlayClicked;
        soloScreen.OnRemove -= SoloScreen_OnRemovingPlayer;
        soloScreen.OnCantRemove -= SoloScreen_OnCantRemovingPlayer;

        popUpAllPlayerNotSet.OnClosePopUp -= popUpAllPlayerNotSet_OnClose;
    }
}
