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

    #region
    public event Action OnClosePopUp;
    #endregion
    public override void Appear()
    {
        base.Appear();

        closeButton.onClick.AddListener(ClosePopUp);
    }

    private void ClosePopUp()
    {
        Disappear();

        closeButton.onClick.RemoveListener(ClosePopUp);

        OnClosePopUp?.Invoke();
    }
}
