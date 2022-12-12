using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "MyGame/Create " + nameof(DebugSettingSO), fileName = nameof(DebugSettingSO))]
public class DebugSettingSO : SingletonScriptableObject<DebugSettingSO>
{
    [TextArea(1, 10)]
    public string document = "・開発デバッグ用の変数を設定できる\n・「DebugSettingSO.asset」はgitignoreで無視しているので、自由に値の変更可\n・ただし、ビルド時には本番用の設定で書き出すように注意\n・OnEnableに本番用の初期値を入れておく\n・DebugCanvasManagerに表示してもしなくてもOK";
    [Header("クリア失敗画面の遅延表示(3秒)")]
    public bool isDelayScreenTrans;
    [Header("ゲーム画面のUI非表示")]
    public bool hideGameCanvas;
    [Header("クリア画面の紙吹雪非表示")]
    public bool hideConfetti;

    [Button("設定リセット")]
    void OnClickResetButton()
    {
        Reset();
    }

    [Button("セーブデータ削除")]
    void OnClickDeleteSaveDataButton()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("セーブデータを削除しました。");
    }

    /// <summary>
    /// 初回ロード時に読み込まれる
    /// 実機では本番用の設定に戻す
    /// https://teratail.com/questions/277014
    /// </summary>
    public void OnEnable()
    {
        if (Application.isEditor) return;
        Reset();
    }

    public void Reset()
    {
        isDelayScreenTrans = false;
        hideGameCanvas = false;
        hideConfetti = false;
    }
}
