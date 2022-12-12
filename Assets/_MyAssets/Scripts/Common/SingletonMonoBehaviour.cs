using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_i == null) _i = FindObjectOfType<T>(true);
            return _i;
        }
    }
    static T _i;
}
