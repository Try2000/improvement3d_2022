using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Create " + nameof(GenreThemeSO), fileName = nameof(GenreThemeSO))]
public class GenreThemeSO : ScriptableObject
{
    [SerializeField] GenreThemeData[] genreThemeDatas;
    public GenreThemeData[] GenreThemeDatas
    {
        get { return genreThemeDatas; }
    }
}
