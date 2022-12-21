using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShowCaseData
{
    public Sprite artistPicture;
    public string showcaseName;
    public string[] names;
    public Genre genre;
}
[CreateAssetMenu(menuName = "MyGame/Create " + nameof(ShowCaseDataSO), fileName = nameof(ShowCaseDataSO))]
public class ShowCaseDataSO : ScriptableObject
{
    [SerializeField] ShowCaseData showCaseData;
    public ShowCaseData ShowCaseData
    {
        get { return showCaseData; }
    }
}
