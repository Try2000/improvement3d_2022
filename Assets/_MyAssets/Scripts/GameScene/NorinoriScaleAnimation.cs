using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NorinoriScaleAnimation : MonoBehaviour
{
    [SerializeField] Vector3 scaleAmount;
    [SerializeField] float duration = 0.4f;
    [SerializeField] Ease easingType = Ease.OutSine;

    private void Awake()
    {
        transform.DOScale(scaleAmount, duration).SetEase(easingType).SetLoops(-1,LoopType.Yoyo);
    }
} 
