using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLoopMover_1 : MonoBehaviour
{
    [SerializeField] Transform movingTransform;
    Transform _transform;


    private void Awake()
    {
        _transform = transform;
    }

    public void StartPathLoop(Transform[] pathTransforms,MoveData moveData)
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            sequence.Append(movingTransform.DOMove(pathTransforms[i].position, moveData.duration).SetEase(moveData.easingType))
                    .AppendInterval(moveData.delay).Pause();
        }
        sequence.SetLoops(-1, LoopType.Restart);
        sequence.Play();
    }
}
