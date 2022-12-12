using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]//エディター起動時に初期化されるように
public class UnitypackageEvents
{
    //コンストラクタ(InitializeOnLoad属性によりエディター起動時に呼び出される)
    static UnitypackageEvents()
    {
        AssetDatabase.importPackageCompleted += ImportCompleted;
        AssetDatabase.importPackageCancelled += ImportCancelled;
        AssetDatabase.importPackageFailed += ImportCallBackFailed;
        AssetDatabase.importPackageStarted += ImportStarted;
    }

    private static void ImportStarted(string packageName)
    {
        // Debug.Log(packageName + "のインポート開始");
    }

    private static void ImportCancelled(string packageName)
    {
        // Debug.Log(packageName + "のインポートキャンセル");
    }

    private static void ImportCallBackFailed(string packageName, string _error)
    {
        // Debug.Log(packageName + "のインポート失敗 : " + _error);
    }

    private static void ImportCompleted(string packageName)
    {
        // Debug.Log(packageName + "のインポート完了 " + UnityTemplatePropertySO.Instance.IsCompleteInitialize);
        if (!packageName.Contains("UnityTemplate")) return;
        if (UnityTemplatePropertySO.Instance.IsCompleteInitialize) return;
        UnityTemplatePropertySO.Instance.IsCompleteInitialize = true;
        SetPlayerSettings();
        AddLayer("Confetti");
    }

    [MenuItem("UnityTemplate/PlayerSettings Initializer")]
    static void OpenPlayerSettingsSetterDialog()
    {
        string message = "PlayerSettingsを自動設定します。\nアプリバージョンが1.0になったりしますが実行しますか？\n\nなお、この機能はテンプレートインポート時に一度実行されるようになっています。";
        if (EditorUtility.DisplayDialog("注意", message, "OK", "キャンセル"))
        {
            SetPlayerSettings();
            AddLayer("Confetti");
        }
    }

    static void SetPlayerSettings()
    {
        // 共通 -----------------------------------------------
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        PlayerSettings.bundleVersion = "1.0";
        PlayerSettings.muteOtherAudioSources = false;

        // iOS ------------------------------------------------        
        PlayerSettings.iOS.buildNumber = "0.1";
        PlayerSettings.iOS.appleDeveloperTeamID = "SU25JZCYM7";
        PlayerSettings.iOS.appleEnableAutomaticSigning = true;
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneOnly;
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 1);// ARM64

        // Android --------------------------------------------
        // 64bit対応
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel22;
        // AndroidSdkVersionsに31が無いため(2022/05/30)
        PlayerSettings.Android.targetSdkVersion = (AndroidSdkVersions)Enum.ToObject(typeof(AndroidSdkVersions), 31);
        PlayerSettings.Android.bundleVersionCode = 1;

        // 変更の自動保存
        AssetDatabase.SaveAssets();
        Debug.Log("Changed PlayerSettings");
    }

    static void AddLayer(string layerName)
    {
        UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            SerializedObject so = new SerializedObject(asset[0]);
            so.Update();
            SerializedProperty layers = so.FindProperty("layers");

            // 最初にみつかった空白を新しいレイヤーで置き換えるようにする
            int indexOfEmptyString = 0;
            for (int i = 0; i < layers.arraySize; ++i)
            {
                // Debug.Log(i + " " + layers.GetArrayElementAtIndex(i).stringValue);
                if (indexOfEmptyString == 0 && layers.GetArrayElementAtIndex(i).stringValue == "")
                {
                    indexOfEmptyString = i;
                }

                // 重複チェック
                if (layers.GetArrayElementAtIndex(i).stringValue == layerName)
                {
                    return;
                }
            }

            int index = indexOfEmptyString;
            layers.GetArrayElementAtIndex(index).stringValue = layerName;
            so.ApplyModifiedProperties();
            so.Update();
        }
    }

}
