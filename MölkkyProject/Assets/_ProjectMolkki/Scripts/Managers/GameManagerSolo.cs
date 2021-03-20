using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    private int maxPoints = 13;
    private int maxMiss = 3;

    private SoloGameScreen gameScreen = null;

    private List<PlayerInfo> allPlayer = default;
    private List<PlayerInfo> playerFinihedGame = default;

    public event Action onInitEnd = default;
    public event Action onQuit = default;
    public event Action<int,PlayerInfo> onConfirmPoint = default;

    public void InitGame(List<PlayerInfo> allPlayer, SoloGameScreen soloGameScreen)
    {
        this.allPlayer = allPlayer;
        gameScreen = soloGameScreen;

        gameScreen.OnAppearEnd += SoloScreenGame_OnEndAppear;

        onInitEnd?.Invoke();
    }

    public void ValidScore()
    {
        gameScreen.CurrentPlayer.Player.Score += gameScreen.CurrentPointSelected;
        gameScreen.onPlayerWin -= GameScreenSolo_OnPlayerWin;
        gameScreen.onPlayerOverPoint -= GameScreenSolo_OnPlayerOverPoint;

        if (gameScreen.CurrentPointSelected == 0)
        {
            gameScreen.CurrentPlayer.Player.Missed++;
        }

        gameScreen.onPlayerWin += GameScreenSolo_OnPlayerWin;
        gameScreen.onPlayerOverPoint += GameScreenSolo_OnPlayerOverPoint;

        gameScreen.UpdateCurrentPlayer();
    }

    private void GameScreenSolo_OnPlayerOverPoint(PlayerInfo player)
    {
        gameScreen.CurrentPlayer.Player.Score = maxPoints / 2;

        gameScreen.UpdateCurrentPlayer();
    }

    private void GameScreenSolo_OnPlayerWin(PlayerInfo player)
    {
        
    }

    #region SoloGameScreen Methodes
    private void SoloScreenGame_OnEndAppear(ScreenObject sender)
    {
        gameScreen.OnAppearEnd -= SoloScreenGame_OnEndAppear;

        gameScreen.onPointClicked += SoloScreenGame_OnPointClicked;
        gameScreen.onQuitClicked += SoloScreenGame_OnQuitClicked;
        gameScreen.onMissedClicked += SoloScreenGame_OnMissedClicked;

        gameScreen.InitPlayers(allPlayer, maxPoints, maxMiss);
        gameScreen.InitButtons();
    }

    private void SoloScreenGame_OnMissedClicked()
    {
        onConfirmPoint?.Invoke(0, gameScreen.CurrentPlayer.Player);
    }

    private void SoloScreenGame_OnQuitClicked()
    {
        onQuit?.Invoke();
    }

    private void SoloScreenGame_OnPointClicked(int point)
    {
        onConfirmPoint?.Invoke(point,gameScreen.CurrentPlayer.Player);
    }
    #endregion

    private void OnDestroy()
    {
        gameScreen.onPointClicked -= SoloScreenGame_OnPointClicked;
        gameScreen.onQuitClicked -= SoloScreenGame_OnQuitClicked;
        gameScreen.onMissedClicked -= SoloScreenGame_OnMissedClicked;
        gameScreen.onPlayerWin -= GameScreenSolo_OnPlayerWin;
        gameScreen.onPlayerOverPoint -= GameScreenSolo_OnPlayerOverPoint;
    }
}
