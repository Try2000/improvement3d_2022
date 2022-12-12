using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create " + nameof(VibrateSettingSO), fileName = nameof(VibrateSettingSO))]
public class VibrateSettingSO : SingletonScriptableObject<VibrateSettingSO>
{
    public SystemSound[] systemSounds;
}

[System.Serializable]
public class SystemSound
{
    public int systemSoundID;
    public string name;
}