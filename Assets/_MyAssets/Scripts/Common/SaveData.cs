using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public static SaveData i => _i;
    private static SaveData _i = new SaveData();

    public bool isOffSE;
    public bool isOffVibration;
    public int coinCount;
}