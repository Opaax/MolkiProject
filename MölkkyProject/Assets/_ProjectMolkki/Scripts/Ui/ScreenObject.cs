using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void ScreenObjectEventHandler(ScreenObject sender);

[RequireComponent(typeof(Animator),typeof(CanvasGroup),typeof(Canvas))]
public class ScreenObject : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] protected Animator animator = null;
    [Space]
    [Header("Canvas")]
    [SerializeField] protected CanvasGroup canvasGroup = null;
    [SerializeField] protected Canvas canvas = null;
    [Space]
    [Header("UnityEvent")]
    [SerializeField] private UnityEvent onAppear;
    [SerializeField] private UnityEvent onAppearEnd;
    [SerializeField] private UnityEvent onDisappear;
    [SerializeField] private UnityEvent onDisappearEnd;

    [Header("HashAnimation")]
    protected static readonly int isAppearHash = Animator.StringToHash("IsAppear");
    protected static readonly int isDisappearHash = Animator.StringToHash("IsDisappear");

    protected bool isAppear = false;

    public event ScreenObjectEventHandler OnAppearEnd;
    public event ScreenObjectEventHandler OnDisappearEnd;

    private RectTransform rectTrans = null;

    public event UnityAction OnApppear
    {
        add { onAppear.AddListener(value); }
        remove { onAppear.RemoveListener(value); }
    }

    public event UnityAction OnDisappear
    {
        add { onDisappear.AddListener(value); }
        remove { onDisappear.RemoveListener(value); }
    }

    private void Awake()
    {
        ScreenManager.allScreen.Add(this);
    }

    public void Init()
    {
        canvasGroup.alpha = 0;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvas.enabled = false;

        isAppear = false;

        rectTrans = GetComponent<RectTransform>();

        SetToCanvas(); 
    }

    protected virtual void SetToCanvas()
    {
        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;

        rectTrans.anchorMin = Vector2.zero;
        rectTrans.anchorMax = Vector2.one;
    }

    public virtual void Appear()
    {
        isAppear = true;

        canvas.enabled = true;
        canvasGroup.interactable = true;

        animator.SetBool(isAppearHash, isAppear);

        onAppear?.Invoke();
    }

    public virtual void EndAppear()
    {
        canvasGroup.blocksRaycasts = true;

        OnAppearEnd?.Invoke(this);
        onAppearEnd?.Invoke();
    }

    public virtual void Disappear()
    {
        isAppear = false;

        canvasGroup.blocksRaycasts = false;

        animator.SetBool(isAppearHash, isAppear);
        animator.SetBool(isDisappearHash, true);

        onDisappear?.Invoke();
    }

    public virtual void EndDisappear()
    {
        canvasGroup.interactable = false;
        canvas.enabled = false;

        animator.SetBool(isDisappearHash, false);

        OnDisappearEnd?.Invoke(this);
        onDisappearEnd?.Invoke();
    }
}
