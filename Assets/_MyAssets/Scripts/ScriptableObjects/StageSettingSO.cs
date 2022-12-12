using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


/// <summary>
/// 【Unity】ステージデータを管理するScriptableObjectで、ステージの並び替えを簡単にする
/// https://qiita.com/engineer_Intern/private/f33ba754aeb3927340bf
/// </summary>

[CreateAssetMenu(menuName = "MyGame/Create " + nameof(StageSettingSO), fileName = nameof(StageSettingSO))]
public class StageSettingSO : SingletonScriptableObject<StageSettingSO>
{
    public StageData[] stageDatas;
}

[Serializable]
public class StageData
{
    string stageNum => "stage " + (Array.IndexOf(StageSettingSO.Instance.stageDatas, this) + 1);
    [LabelText("$stageNum")] public string data = "ここはゲームの設計によって任意に変更(csvなど)";
}