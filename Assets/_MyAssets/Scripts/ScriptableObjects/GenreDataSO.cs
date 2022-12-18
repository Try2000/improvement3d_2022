using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Genre
{
    Style,
    JHH,
    House,
    SHH,
    Lock,
    Pop,
    Hiphop,
    Waack,
    Break
}
[System.Serializable]
public class GenreData
{
    public Genre genre;
    public NeonColorSO neonColorSO;

}
public class GenreDataSO : ScriptableObject
{
    [SerializeField] GenreData genreData;
    public GenreData GenreData
    {
        get { return genreData; }
    }
}
