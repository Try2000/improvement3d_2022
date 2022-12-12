using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/// <summary>
/// https://docs.unity3d.com/ja/2018.4/Manual/RunningEditorCodeOnLaunch.html
/// Unityがロードされたとき、およびスクリプトが再コンパイルされたときに呼ばれる
/// </summary>
[InitializeOnLoad]
public class Startup
{
    static Startup()
    {
        // Debug.Log("Up and running");
        CreateDebugSettingSO();
        InputKeystore();
        // SetQualitySettings();
    }

    static void CreateDebugSettingSO()
    {
        if (DebugSettingSO.Instance) return;
        var path = "Assets/_MyAssets/Resources/ScriptableObjects/DebugSettingSO.asset";
        var setting = ScriptableObject.CreateInstance<DebugSettingSO>(); // ScriptableObjectはnewではなくCreateInstanceを使います
        AssetDatabase.CreateAsset(setting, path);
        Debug.Log("Created DebugSettingSO.asset");
    }

    /// <summary>
    /// Unity5 Android Build keystore 自動入力
    /// http://yasuaki-ohama.hatenablog.com/entry/2015/12/23/213956
    /// </summary>
    static void InputKeystore()
    {
        //エイリアス名
        PlayerSettings.Android.keyaliasName = "key";
        // パスワードの再設定
        PlayerSettings.Android.keystorePass = "Baba1105";
        // パスワードの再設定
        PlayerSettings.Android.keyaliasPass = "Baba1105";
    }

    static void SetQualitySettings()
    {
        QualitySettings.SetQualityLevel(4);// Unityエディタ上では多分できない
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}
