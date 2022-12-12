using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.EventSystems;

/// <summary>
/// 画面UIの一括管理
/// GameDirectorと各画面を中継する役割
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] Transform canvasesParentTf;
    [Header("スプラッシュあり")]
    [SerializeField] bool isShowSplash;
    [Header("シーンロード直後に開く画面")]
    [SerializeField] ScreenState initializeScreen;
    [Header("ステージ遷移がマルチシーンかシングルシーンか")]
    [SerializeField] bool isMultiScene;
    [SerializeField] EventSystem eventSystem;
    BaseCanvasManager[] baseCanvasManagers;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        baseCanvasManagers = canvasesParentTf.GetComponentsInChildren<BaseCanvasManager>(true);
    }

    void Start()
    {
        SaveDataManager.i.LoadSaveData();
        StageTransManager.i.LoadStageOnAppLaunch(
            startStageNum: 1,
            lastStageNum: 1,//マルチシーンのときは無視してOK
            isMultiScene: isMultiScene);
        //debug画面でlastStageNumを使うので、こっちの方が後
        SetCanvases();
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        if (isShowSplash)
        {
            Variables.screenState = ScreenState.Splash;
        }
        else
        {
            StageTransManager.i.ReLoadStage();
        }
    }

    void SetCanvases()
    {
        foreach (var baseCanvasManager in baseCanvasManagers)
        {
            baseCanvasManager.OnStart();
        }
    }

    void Update()
    {
        foreach (var baseCanvasManager in baseCanvasManagers)
        {
            if (!baseCanvasManager.IsThisScreen) continue;
            baseCanvasManager.OnUpdate();
        }
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        foreach (var baseCanvasManager in baseCanvasManagers)
        {
            baseCanvasManager.OnSceneLoaded();
        }
        Variables.screenState = initializeScreen;

        CheckEventSystemDuplication();
        RemoveCameraCullingMask();
    }

    void CheckEventSystemDuplication()
    {
        eventSystem.gameObject.SetActive(true);
        var eventSystems = FindObjectsOfType<EventSystem>();
        bool isDuplicate = eventSystems.Length > 1;
        eventSystem.gameObject.SetActive(!isDuplicate);
    }

    void RemoveCameraCullingMask()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (var camera in cameras)
        {
            if (camera == UICameraController.i.cam) continue;
            camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Confetti"));
        }

    }
}