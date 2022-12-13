using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ThickCircleData
{
    public string name;
    public ThickCircleAnimator thickCircle;
    public ThickCircleAnimationData animationData;
}
public class ThickCircleAnimateManager : MonoBehaviour
{
    [SerializeField] ThickCircleData[] thickCircleDatas;
    private void Awake()
    {
        Array.ForEach(thickCircleDatas, thickCircleData =>
         {
             thickCircleData.thickCircle.gameObject.SetActive(false);
         });
    }
    public void PlayThickCircle(string name)
    {
        ThickCircleData thickCircleData = GetCircleData(name);
        if (thickCircleData == null) return;
        thickCircleData.thickCircle.gameObject.SetActive(true);
        thickCircleData.thickCircle.ThickCircleAnimationData = thickCircleData.animationData;
        thickCircleData.thickCircle.AnimateCircle();
    }
    ThickCircleData GetCircleData(string name)
    {
        return Array.Find(thickCircleDatas, thickCircleData => thickCircleData.name == name);
    }
}
