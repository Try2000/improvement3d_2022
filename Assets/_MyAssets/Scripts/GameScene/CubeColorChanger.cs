using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] string varname;

    private void Awake()
    {
        GenreThemeColorSetter.Instance.onColorChanged += ChangeColor;
    }

    public void ChangeColor(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Debug.Log(renderers.Length);
        Array.ForEach(renderers, renderer =>
        {
            renderer.material.SetColor(varname, color);
        });
    }
}
