using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct MissInfoView
{
    [Header("base struct")]
    public Transform missedTransform;
    public Image imageFeedBack;
    [Space]
    [Header("Tween Settings")]
    [SerializeField] float timeToFade;
    [SerializeField] Ease easeFade;
    [SerializeField,Tooltip("Optinal; should be set if 'useFadeColor'")] private Color colorToFade;
    [Space]
    [Header("TweenOptions")]
    [SerializeField] private bool usefadeColor;

    public void AppearImage()
    {
        if(usefadeColor)
        {
            imageFeedBack.DOColor(colorToFade, .1f);
            return;
        }

        imageFeedBack.DOFade(1, timeToFade).SetEase(easeFade).SetAutoKill(true);
    }

    public void DisappearImage()
    {
        if (usefadeColor)
        {
            imageFeedBack.DOColor(Color.white, .1f);
            return;
        }

        imageFeedBack.DOFade(0, .1f).SetAutoKill(true);
    }
}
