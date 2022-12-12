using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "MyGame/Create " + nameof(UnityTemplatePropertySO), fileName = nameof(UnityTemplatePropertySO))]
public class UnityTemplatePropertySO : SingletonScriptableObject<UnityTemplatePropertySO>
{

    [SerializeField] string version;
    public string Version => version;
    [SerializeField] string documentURL;
    public string DocumentURL => documentURL;
    [SerializeField] string documentURL_android;
    public string DocumentURL_android => documentURL_android;
    [SerializeField] string documentURL_iOS;
    public string DocumentURL_iOS => documentURL_iOS;
    [SerializeField] string documentURL_facebookSDK;
    public string DocumentURL_facebookSDK => documentURL_facebookSDK;

    [ReadOnly] public bool IsCompleteInitialize;
}
