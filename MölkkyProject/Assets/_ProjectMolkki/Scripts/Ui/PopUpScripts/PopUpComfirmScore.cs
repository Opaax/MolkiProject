using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpComfirmScore : PopUpObject
{
    [SerializeField] private Button confirmButton = null;
    [Space]
    [Header("PopUpSettings")]
    [SerializeField] private TextMeshProUGUI topText = null;
    [SerializeField] private TextMeshProUGUI currentScore = null;
    [SerializeField] private TextMeshProUGUI nextScore = null;
    [SerializeField] private MissInfoView[] currentMiss = null;
    [Space]
    [SerializeField] private MissInfoView[] nextMiss = null;

    private Tween feedBackTween = default;

    public event Action OnConfirmClick = default;

    public override void Appear()
    {
        ResetImageFeedBack();

        confirmButton.onClick.AddListener(OnConfirmClicked);

        base.Appear();
    }
    public override void Disappear()
    {
        base.Disappear();
    }

    private void OnConfirmClicked()
    {
        confirmButton.onClick.RemoveListener(OnConfirmClicked);

        feedBackTween.SetLoops(0);
        Debug.Log(feedBackTween.target);
        ResetLoopTransForm((Transform)feedBackTween.target);
        feedBackTween.Complete();

        OnConfirmClick?.Invoke();

        ClosePopUp();
    }

    private void ResetImageFeedBack()
    {
        for (int i = currentMiss.Length - 1; i >= 0; i--)
        {
            currentMiss[i].DisappearImage();
            nextMiss[i].DisappearImage();
        }
    }


    public void SetUpPopUp(PlayerInfo player, int newScore)
    {
        topText.text = player.Pseudo;

        currentScore.text = player.Score.ToString();
        nextScore.text = (player.Score + newScore).ToString();

        for (int i = player.Missed - 1; i >= 0; i--)
        {
            currentMiss[i].AppearImage();
            nextMiss[i].AppearImage();
        }

        if(newScore == 0)
        {
            nextMiss[player.Missed].AppearImage();

            feedBackTween = nextMiss[player.Missed].missedTransform.DOScale(1.3f, .4f).SetLoops(-1,LoopType.Yoyo).OnComplete(() => ResetLoopTransForm(nextMiss[player.Missed].missedTransform));
        }
        else
        {
            feedBackTween = nextScore.transform.DOScale(1.3f, .4f).SetLoops(-1, LoopType.Yoyo).OnComplete(() => ResetLoopTransForm(nextScore.transform));
        }

        feedBackTween.SetAutoKill(true);
    }

    private void ResetLoopTransForm(Transform transformToReset)
    {
        transformToReset.localScale = Vector3.one;

        feedBackTween.Kill();
    }
}