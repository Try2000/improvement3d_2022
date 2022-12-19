using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PathLoopMover : MonoBehaviour
{
    [SerializeField] Transform[] pathTransforms;
    [SerializeField] int initialIndex;
    [SerializeField] MoveData moveData;
    Transform _transform;
    

    private void Awake()
    {
        _transform = transform;
        int nextPathIndex = initialIndex;
        Sequence sequence = DOTween.Sequence();
        for(int i  = 0; i < pathTransforms.Length; i++)
        {
            sequence.Append(_transform.DOMove(pathTransforms[nextPathIndex].position, moveData.duration).SetEase(moveData.easingType))
                    .AppendInterval(moveData.delay).Pause();
            nextPathIndex++;
            if (nextPathIndex >= pathTransforms.Length) nextPathIndex = 0;
        }
        sequence.SetLoops(-1,LoopType.Restart);
        sequence.Play();
    }
}
