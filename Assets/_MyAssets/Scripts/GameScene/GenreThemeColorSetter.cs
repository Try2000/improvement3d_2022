using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class ColorChangeData
{
    public Renderer renderer;
    public string varname;
}
public class GenreThemeColorSetter : SingletonMonoBehaviour<GenreThemeColorSetter>
{
    [SerializeField] GenreThemeSO genreThemeSO;
    [SerializeField] Genre genre;
    [SerializeField] ShowCaseDataSO showCaseDataSO;
    [SerializeField] ColorChangeData[] colorChangeDatas;
    public event Action<Color> onColorChanged;

    private void Start()
    {

        GenreThemeData genreThemeData = Array.Find(genreThemeSO.GenreThemeDatas, genreThemeData =>
        {
            if (showCaseDataSO == null) return genreThemeData.genre == genre;
            else return genreThemeData.genre == showCaseDataSO.ShowCaseData.genre;
        }
        );
        if (onColorChanged != null) onColorChanged(genreThemeData.color);
        Array.ForEach(colorChangeDatas, colorChangeData =>
        {
            colorChangeData.renderer.material.SetColor(colorChangeData.varname, genreThemeData.color);
        });
    }
}
