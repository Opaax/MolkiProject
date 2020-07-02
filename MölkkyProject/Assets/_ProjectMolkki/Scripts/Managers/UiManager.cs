using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private ScreenManager screenManager = new ScreenManager();

    private void Start()
    {
        screenManager.isAllScreensInit += ScreenManager_AllScreenInit;

        screenManager.Init();
    }

    private void ScreenManager_AllScreenInit()
    {
        this.Log("AllInit");
    }
}
