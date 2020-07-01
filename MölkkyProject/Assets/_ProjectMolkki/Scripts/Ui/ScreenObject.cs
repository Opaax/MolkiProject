using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(CanvasGroup),typeof(Canvas))]
public class ScreenObject : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator animator = null;
    [Space]
    [Header("Canvas")]
    [SerializeField] CanvasGroup canvasGroup = null;
    [SerializeField] Canvas canvas = null;

    private RectTransform rectTrans = null;

    private void Awake()
    {
        ScreenManager.allScreen.Add(this);
    }

    public void Init()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        rectTrans = GetComponent<RectTransform>();

        SetToCanvas(); 
    }

    private void Start()
    {
        Init();
    }

    protected virtual void SetToCanvas()
    {
        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;

        rectTrans.anchorMin = Vector2.zero;
        rectTrans.anchorMax = Vector2.one;
    }
}
