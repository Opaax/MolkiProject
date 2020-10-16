using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAllPlayerNotSet : PopUpObject
{

    public override void Appear()
    {
        base.Appear();

        Debug.Log("AppearAllPlayerNotSet");

    }

    protected override void ClosePopUp()
    {
        base.ClosePopUp();

        Debug.Log("CloseAllPlayerNotSet");
    }
}
