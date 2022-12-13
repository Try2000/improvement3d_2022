using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoundTextData
{
    public string name;
    public RoundTextAnimator roundTextAnimator;
    public RoundTextAnimationData roundTextAnimationData;
}
[RequireComponent(typeof(GeneralMarkerReceiver))]
public class RoundTextManager : MonoBehaviour
{
    [SerializeField] RoundTextData[] roundTextDatas;

    private void Awake()
    {
        if(TryGetComponent<GeneralMarkerReceiver>(out GeneralMarkerReceiver generalMarkerReceiver))
        {
            generalMarkerReceiver.onNotify += OnNotify;
        }
        Init();
    }
    void Init()
    {
        Array.ForEach(roundTextDatas, roundTextData =>
         {
             roundTextData.roundTextAnimator.gameObject.SetActive(false);
             roundTextData.roundTextAnimator.RoundTextAnimationData = roundTextData.roundTextAnimationData;
         });
    } 
    public void OnNotify(string name)
    {
        RoundTextData roundTextData = GetTextData(name);
        if (roundTextData == null) return;
        roundTextData.roundTextAnimator.gameObject.SetActive(true);
        roundTextData.roundTextAnimator.AnimateRoundText();

    }
    RoundTextData GetTextData(string name)
    {
        return Array.Find(roundTextDatas, textData => textData.name == name);
    }
}
