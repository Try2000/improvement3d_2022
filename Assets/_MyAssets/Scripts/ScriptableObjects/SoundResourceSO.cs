using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(menuName = "MyGame/Create " + nameof(SoundResourceSO), fileName = nameof(SoundResourceSO))]
public class SoundResourceSO : SingletonScriptableObject<SoundResourceSO>
{
    [ListDrawerSettings
    (
        Expanded = true,
        ShowIndexLabels = true
    )]
    public SoundResource[] resources;
}

[System.Serializable]
public class SoundResource
{
    public AudioClip audioClip;
    public string name;
}