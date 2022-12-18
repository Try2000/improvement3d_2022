using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LoopMover : MonoBehaviour
{
    [SerializeField] MoveData defaultMoveData;
    [SerializeField] float randomDurationMax = 1;
    [SerializeField] bool isAutoStart = false;
    MoveData moveData;
    public MoveData MoveData
    {
        set { moveData = value; }
    }
    Transform _transform;

    private void Awake()
    {
        _transform = transform;
        moveData = defaultMoveData;
        if (isAutoStart) StartMove();
    }

    public void StartMove()
    {
        float randomVal = UnityEngine.Random.Range(0, randomDurationMax);
        Sequence sequence = DOTween.Sequence().Append(_transform.DOMove(moveData.moveAmount, moveData.duration + randomVal).SetEase(moveData.easingType).SetRelative())
                                             .Append(_transform.DOMove(-moveData.moveAmount, moveData.duration + randomVal).SetEase(moveData.easingType).SetRelative()).SetLoops(-1).SetLink(gameObject);
    }
}
