using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class UnityTemplateMenuItem
{
    [MenuItem("UnityTemplate/Version")]
    static void Version()
    {
        EditorUtility.DisplayDialog("Version", "UnityTemplate_v" + UnityTemplatePropertySO.Instance.Version, "OK");
    }

    [MenuItem("UnityTemplate/Documents/UnityTemplate")]
    static void OpenDocuments()
    {
        Application.OpenURL(UnityTemplatePropertySO.Instance.DocumentURL);
    }

    [MenuItem("UnityTemplate/Documents/android setting")]
    static void OpenDocuments_android()
    {
        Application.OpenURL(UnityTemplatePropertySO.Instance.DocumentURL_android);
    }

    [MenuItem("UnityTemplate/Documents/iOS setting")]
    static void OpenDocuments_iOS()
    {
        Application.OpenURL(UnityTemplatePropertySO.Instance.DocumentURL_iOS);
    }

    [MenuItem("UnityTemplate/Documents/facebookSDK integration")]
    static void OpenDocuments_facebookSDK()
    {
        Application.OpenURL(UnityTemplatePropertySO.Instance.DocumentURL_facebookSDK);
    }
}
