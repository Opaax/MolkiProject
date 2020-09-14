using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSetPlayerInfo : PopUpObject
{
    [Header("Buttons")]
    [SerializeField] private Button buttonRemove = null;
    [Space]
    [Header("PseudoRelative")]
    [SerializeField] private TMP_InputField pseudoField = null;

    private PlayerInfo currentPlayerInfo = null;

    public event Action<PlayerInfo> onRemoveClicked = default;

    public override void EndAppear()
    {
        base.EndAppear();

        AddButtonEvent();
    }

    private void AddButtonEvent()
    {
        buttonRemove.onClick.AddListener(OnRemoveClicked);
    }

    private void OnRemoveClicked()
    {
        buttonRemove.onClick.RemoveListener(OnRemoveClicked);

        onRemoveClicked?.Invoke(currentPlayerInfo);

        ClosePopUp();
    }

    protected override void ClosePopUp()
    {
        base.ClosePopUp();

        CheckTextField();

        currentPlayerInfo.CheckPseudoColor();
    }

    private void CheckTextField()
    {
        if (pseudoField.text == "")
            currentPlayerInfo.Pseudo = currentPlayerInfo.DefaultString;
    }

    public void SetPlayerInfo(PlayerInfo currentPlayerInfo)
    {
        this.currentPlayerInfo = currentPlayerInfo;

        if (currentPlayerInfo.Pseudo != "")
            pseudoField.text = currentPlayerInfo.Pseudo;
    }

    public void SetPlayerPseudo ()
    {
        currentPlayerInfo.Pseudo = pseudoField.text;
    }
}
