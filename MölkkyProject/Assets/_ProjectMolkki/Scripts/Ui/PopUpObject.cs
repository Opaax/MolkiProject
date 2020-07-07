using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpObject : ScreenObject
{
    [Space]
    [Header("CloseButton")]
    [SerializeField] private Button closeButton = null;

    public override void Appear()
    {
        base.Appear();

        closeButton.onClick.AddListener(ClosePopUp);
    }

    private void ClosePopUp()
    {
        EndAppear();
    }

    public override void EndAppear()
    {
        base.EndAppear();

        closeButton.onClick.RemoveListener(ClosePopUp);
    }
}
