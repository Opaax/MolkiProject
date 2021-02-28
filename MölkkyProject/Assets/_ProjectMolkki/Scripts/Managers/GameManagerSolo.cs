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

    public void InitGame(List<PlayerInfo> allPlayer, SoloGameScreen soloGameScreen)
    {
        this.allPlayer = allPlayer;
        gameScreen = soloGameScreen;

        gameScreen.OnAppearEnd += SoloScreenGame_OnEndAppear;

        onInitEnd?.Invoke();
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
        this.Log("OnMissedClicked");
    }

    private void SoloScreenGame_OnQuitClicked()
    {
        this.Log("OnQuitClicked");

        onQuit?.Invoke();
    }

    private void SoloScreenGame_OnPointClicked(int point)
    {
        this.Log($"on{point}PointClicked");
    }
    #endregion

    private void OnDestroy()
    {
        gameScreen.onPointClicked -= SoloScreenGame_OnPointClicked;
        gameScreen.onQuitClicked -= SoloScreenGame_OnQuitClicked;
        gameScreen.onMissedClicked -= SoloScreenGame_OnMissedClicked;
    }
}
