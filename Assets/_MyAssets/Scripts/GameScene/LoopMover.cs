using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LoopMover : MonoBehaviour
{
    [SerializeField] Vector3 moveAmount;
    [SerializeField] float duration = 1;
    [SerializeField] Ease easingType = Ease.OutQuad;
    [SerializeField] float randomDurationMax = 1;

    private void Awake()
    {
        Transform _transform = transform;
        float randomVal = UnityEngine.Random.Range(0, randomDurationMax);
        Sequence sequence = DOTween.Sequence().Append(_transform.DOMove(moveAmount, duration + randomVal).SetEase(easingType).SetRelative())
                                              .Append(_transform.DOMove(-moveAmount, duration + randomVal).SetEase(easingType).SetRelative()).SetLoops(-1).SetLink(gameObject);
    }
}
