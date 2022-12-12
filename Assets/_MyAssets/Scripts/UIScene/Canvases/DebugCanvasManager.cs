using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvasManager : BaseCanvasManager
{
    [Header("バナー")]
    [SerializeField] Button clearButton;
    [SerializeField] Button failButton;
    [SerializeField] Button hideBannerButton;
    [SerializeField] Button debugButton;
    [SerializeField] Image bannerImage;
    [SerializeField] Button restartButton;

    [Header("デバッグ画面")]
    [SerializeField] Text templateVersionText;
    [SerializeField] Image debugPanel;
    [SerializeField] Button applyButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button resetButton;
    [SerializeField] Toggle screenDelayToggle;
    [SerializeField] Toggle hideGameCanvasToggle;
    [SerializeField] Toggle hideConfettiToggle;
    [SerializeField] Toggle resetSaveDataToggle;
    [SerializeField] Dropdown stageNumDd;
    [SerializeField] InputField framerateInputField;


    public override void OnStart()
    {
        gameObject.SetActive(Debug.isDebugBuild);
        templateVersionText.text += UnityTemplatePropertySO.Instance.Version;
        hideBannerButton.onClick.AddListener(OnClickHideBannerButton);
        debugButton.onClick.AddListener(OnClickDebugButton);
        debugPanel.gameObject.SetActive(false);
        applyButton.onClick.AddListener(OnClickApplyButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        resetButton.onClick.AddListener(OnClickResetButton);
        restartButton.onClick.AddListener(OnClickRestartButton);
        clearButton.onClick.AddListener(OnClickClearButton);
        failButton.onClick.AddListener(OnClickFailButton);
        stageNumDd.ClearOptions();
        stageNumDd.AddOptions(StageTransManager.i.GetStageNames);
    }


    public override void OnSceneLoaded()
    {
    }

    public override void OnUpdate()
    {
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
    }


    /// <summary>
    /// inputfieldとかtoggleにデータを入れる
    /// </summary>
    void ShowParam()
    {
        screenDelayToggle.isOn = DebugSettingSO.Instance.isDelayScreenTrans;
        hideGameCanvasToggle.isOn = DebugSettingSO.Instance.hideGameCanvas;
        hideConfettiToggle.isOn = DebugSettingSO.Instance.hideConfetti;
        resetSaveDataToggle.isOn = false;
        stageNumDd.value = StageTransManager.i.CurrentStageNum - 1;
        framerateInputField.text = Application.targetFrameRate.ToString();
    }

    void OnClickHideBannerButton()
    {
        bannerImage.gameObject.SetActive(!bannerImage.gameObject.activeSelf);
        hideBannerButton.GetComponent<CanvasGroup>().alpha = bannerImage.gameObject.activeSelf ? 1 : 0;
    }

    void OnClickDebugButton()
    {
        debugPanel.gameObject.SetActive(true);
        ShowParam();
    }

    void OnClickApplyButton()
    {
        DebugSettingSO.Instance.isDelayScreenTrans = screenDelayToggle.isOn;
        DebugSettingSO.Instance.hideGameCanvas = hideGameCanvasToggle.isOn;
        DebugSettingSO.Instance.hideConfetti = hideConfettiToggle.isOn;
        if (int.TryParse(framerateInputField.text, out int targetFrameRate))
        {
            Application.targetFrameRate = targetFrameRate;
        }
        TransStage();
        SaveDataManager.i.Save();
        //削除後にセーブ処理を入れると、データが復活する
        if (resetSaveDataToggle.isOn) { PlayerPrefs.DeleteAll(); }
        Close();
    }

    void OnClickCancelButton()
    {
        Close();
    }

    void OnClickResetButton()
    {
        DebugSettingSO.Instance.Reset();
        ShowParam();
    }

    void Close()
    {
        debugPanel.gameObject.SetActive(false);
    }

    void TransStage()
    {
        StageTransManager.i.CurrentStageNum = stageNumDd.value + 1;
        StageTransManager.i.ReLoadStage();
    }

    void OnClickRestartButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        StageTransManager.i.ReLoadStage();
    }
    void OnClickClearButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        Variables.screenState = ScreenState.Clear;
    }
    void OnClickFailButton()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        Variables.screenState = ScreenState.Failed;
    }
}
