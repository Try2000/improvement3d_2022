using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ClearCanvasManager : BaseCanvasManager
{
    [SerializeField] Button nextButton;
    [SerializeField] Image dummyRectangleImage;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Clear);

        nextButton.onClick.AddListener(OnClickNextButton);
        gameObject.SetActive(false);
        dummyRectangleImage.gameObject.SetActive(Debug.isDebugBuild);
    }

    public override void OnUpdate()
    {

    }

    protected override void OnOpen()
    {
        float delay = DebugSettingSO.Instance.isDelayScreenTrans ? 3 : 0;

        DOTween.Sequence()
        .AppendInterval(0f)// クリア判定してから紙吹雪が出るまでのインターバル
        .AppendCallback(() =>
        {
            UICameraController.i.PlayConfetti();
            SoundManager.i.PlayOneShot(0);
        })
        .AppendInterval(1.2f + delay)// 紙吹雪が出てからクリア画面が出るまでのインターバル
        .AppendCallback(() =>
        {
            gameObject.SetActive(true);
        });
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
        UICameraController.i.ShowConfetti(show: false);
    }

    public override void OnSceneLoaded()
    {

    }

    void OnClickNextButton()
    {
        StageTransManager.i.LoadNextStage();
    }
}
