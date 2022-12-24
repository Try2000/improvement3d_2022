using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuwaFuwaAnimation : MonoBehaviour
{
    [SerializeField] Vector3 moveAmount;
    [SerializeField] float duration = 0.4f;
    [SerializeField] Ease easingType = Ease.OutSine;

    private void Awake()
    {
        transform.DOMove(moveAmount, duration).SetRelative().SetEase(easingType).SetLoops(-1, LoopType.Yoyo);
    }
}
