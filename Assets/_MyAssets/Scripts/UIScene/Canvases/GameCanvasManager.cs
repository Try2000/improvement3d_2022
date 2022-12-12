using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text stageNumText;
    [SerializeField] Button retryButton;

    public override void OnStart()
    {

        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(currentStageNum => StageTransManager.i.CurrentStageNum)
            .Subscribe(currentStageNum => { ShowStageNumText(currentStageNum); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);
        retryButton.onClick.AddListener(OnClickRetryButton);
    }

    public override void OnUpdate()
    {

    }

    protected override void OnOpen()
    {

    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    public override void OnSceneLoaded()
    {
        gameObject.SetActive(!DebugSettingSO.Instance.hideGameCanvas);
    }

    void ShowStageNumText(int stageNum)
    {
        stageNumText.text = "LEVEL " + stageNum.ToString("000");
    }

    void OnClickRetryButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        //AdManager.i.Interstitial.ShowInterstitial();
        StageTransManager.i.ReLoadStage();
    }
}
