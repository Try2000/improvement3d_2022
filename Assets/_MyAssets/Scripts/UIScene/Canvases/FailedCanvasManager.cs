using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class FailedCanvasManager : BaseCanvasManager
{
    [SerializeField] Button restartButton;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Failed);
        restartButton.onClick.AddListener(OnClickRestartButton);
        gameObject.SetActive(false);
    }

    public override void OnUpdate()
    {

    }

    protected override void OnOpen()
    {
        float delay = DebugSettingSO.Instance.isDelayScreenTrans ? 3 : 0;
        DOTween.Sequence()
       .AppendInterval(1.2f + delay)
       .AppendCallback(() =>
       {
           gameObject.SetActive(true);
       });
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    public override void OnSceneLoaded()
    {

    }

    void OnClickRestartButton()
    {
        StageTransManager.i.ReLoadStage();
    }
}
