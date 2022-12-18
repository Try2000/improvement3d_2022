using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMoveGetter : MoveAlgorythmGetterBase
{
    [SerializeField] int max = 100;
    [SerializeField] Vector3 moveAmountMax;
    [SerializeField] int quotient = 4;
    [SerializeField] float speed = 0.5f;
    [SerializeField] float delayMax = 0.8f;
    [SerializeField] Ease easingType = Ease.Linear;
    public override MoveData GetMoveData(int num)
    {
        int tmp = (Mathf.CeilToInt((float)(num) / quotient)) * quotient;
        float rate = (float) tmp / (float ) max ;
        Vector3 moveAmount = Mathf.Sin(rate) * moveAmountMax;
        return new MoveData(moveAmount, moveAmount.magnitude / speed, easingType, true,delayMax * rate);
    }
}
