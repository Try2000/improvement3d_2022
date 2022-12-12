using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
# if UNITY_IOS
using UnityEditor.iOS.Xcode;
# endif
using System.IO;
using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class MyBuildPostprocessor : IPreprocessBuildWithReport
{
    // 実行順
    public int callbackOrder { get { return 0; } }

    static string releaseBundleIdentifier;
    static string releaseBundleDisplayName;

    // ビルド前処理
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("OnPreprocessBuild");
        OnPreprocessBuild_Android(report);
    }

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        Debug.Log("OnPostProcessBuild buildTarget : " + buildTarget);
        OnPostProcessBuild_IOS(buildTarget, path);
        OnPostProcessBuild_Android(buildTarget, path);
    }

    public void OnPreprocessBuild_Android(BuildReport report)
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) return;
        Debug.Log("OnPreprocessBuild_Android");
        releaseBundleIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);

        if (releaseBundleIdentifier.Contains(".dev"))
        {
            releaseBundleIdentifier = releaseBundleIdentifier.Replace(".dev", "");
        }

        if (releaseBundleIdentifier.Contains("DefaultCompany"))
        {
            EditorUtility.DisplayDialog("UnityTemplate", "bundle idが設定されていません", "OK");
            Debug.LogError("bundle idが設定されていません");
        }


        // ビルド時に一時的にPlayerSettingを変更し、ビルド後に戻す
        // androidでのポストプロセスが実装できなかったため、この方法ならOSに関わらず実装できる
        // TODO: ただし、ビルド中にgitの変更に出るので、間違ってコミットする可能性あり
        if (EditorUserBuildSettings.development)
        {
            releaseBundleDisplayName = PlayerSettings.productName;

            string dateName = DateTime.Today.Month.ToString("D2") + DateTime.Today.Day.ToString("D2");

            string debugBundleDisplayName = $"{dateName}_{releaseBundleDisplayName}";
            string debugBundleIdentifier = releaseBundleIdentifier + ".dev";

            PlayerSettings.productName = debugBundleDisplayName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, debugBundleIdentifier);
        }
    }

    static void OnPostProcessBuild_Android(BuildTarget buildTarget, string path)
    {
        if (buildTarget != BuildTarget.Android) return;
        // https://qiita.com/ckazu/items/07dff39449e9f544b038
        Debug.Log("OnPostProcessBuild_Android");

        if (EditorUserBuildSettings.development)
        {
            PlayerSettings.productName = releaseBundleDisplayName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, releaseBundleIdentifier);
        }
        // PostProcessBuild後の保存が自動でされず、gitの変更にPreprocessBuildの変更が出てしまうため
        AssetDatabase.SaveAssets();
    }

    static void OnPostProcessBuild_IOS(BuildTarget buildTarget, string path)
    {
        if (buildTarget != BuildTarget.iOS) return;
#if UNITY_IOS
        string projectPath = PBXProject.GetPBXProjectPath(path);

        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(projectPath);

        //Exception: Calling TargetGuidByName with name='Unity-iPhone' is deprecated.【解決策】
        //https://koujiro.hatenablog.com/entry/2020/03/16/050848
        string target = pbxProject.GetUnityMainTargetGuid();


        //pbxProject.AddCapability(target, PBXCapabilityType.InAppPurchase);

        // Plistの設定のための初期化
        var plistPath = Path.Combine(path, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        if (Debug.isDebugBuild)
        {
            //日付とか
            string dateName = DateTime.Today.Month.ToString("D2") + DateTime.Today.Day.ToString("D2");
            string timeName = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2");

            //アプリ名
            plist.root.SetString("CFBundleDisplayName", $"{dateName}_{Application.productName}");

            //bundleId
            pbxProject.SetBuildProperty(target, "PRODUCT_BUNDLE_IDENTIFIER", Application.identifier + ".dev");
        }

        plist.WriteToFile(plistPath);
        pbxProject.WriteToFile(projectPath);
#endif
    }
}