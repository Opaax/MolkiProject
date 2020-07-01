using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager
{
    private List<ScreenObject> _currentScreenOpen = new List<ScreenObject>(); 

    public static List<ScreenObject> allScreen = new List<ScreenObject>();

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
    }
}
