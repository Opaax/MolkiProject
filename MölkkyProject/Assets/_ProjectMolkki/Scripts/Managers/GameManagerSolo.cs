using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSolo : MonoBehaviour
{
    private SoloGameScreen gameScreen = null;

    private List<PlayerInfo> allPlayer = default;

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

        if(gameScreen.CurrentPointSelected == 0)
        {
            gameScreen.CurrentPlayer.Player.Missed++;
        }

        gameScreen.UpdateCurrentPlayer();
    }

    #region SoloGameScreen Methodes
    private void SoloScreenGame_OnEndAppear(ScreenObject sender)
    {
        gameScreen.OnAppearEnd -= SoloScreenGame_OnEndAppear;

        gameScreen.onPointClicked += SoloScreenGame_OnPointClicked;
        gameScreen.onQuitClicked += SoloScreenGame_OnQuitClicked;
        gameScreen.onMissedClicked += SoloScreenGame_OnMissedClicked;

        gameScreen.InitPlayer(allPlayer);
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
    }
}
