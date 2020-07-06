using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesScreen : ScreenObject
{
    [Space]
    [Header("Button")]
    [SerializeField] private Button buttonBack = null;
    [Space]
    [Header("ScrollingArea")]
    [SerializeField] private RectTransform scrollingContent = null;

    public override void EndDisappear()
    {
        base.EndDisappear();
        scrollingContent.position = new Vector3(scrollingContent.position.x, 0);
    }
}
