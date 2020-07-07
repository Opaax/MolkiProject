using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager
{
    private List<ScreenObject> _currentScreenOpen = new List<ScreenObject>(); 

    public static List<ScreenObject> allScreen = new List<ScreenObject>();

    public event Action isAllScreensInit;

    public List<ScreenObject> CurrentScreenOpen { get => _currentScreenOpen; }

    public void Init()
    {
        InitScreens();
    }

    private void InitScreens()
    {
        for (int i = allScreen.Count - 1; i >= 0; i--)
        {
            allScreen[i].Init();
        }

        isAllScreensInit?.Invoke();
    }

    public void AddActifScreen (ScreenObject screen)
    {
        _currentScreenOpen.Add(screen);
    }

    public void RemoveInactifScreen (ScreenObject screen)
    {
        if (_currentScreenOpen.Contains(screen))
            _currentScreenOpen.Remove(screen);
    }
}
