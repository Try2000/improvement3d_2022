using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeonColorData
{
    public Color neonColor;
}
public class NeonColorSO : ScriptableObject
{
    [SerializeField] NeonColorData neonColorData;
    public NeonColorData NeonColorData
    {
        get { return neonColorData; }
    }
}
