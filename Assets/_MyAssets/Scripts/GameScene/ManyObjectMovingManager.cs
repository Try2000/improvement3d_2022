using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyObjectMovingManager : MonoBehaviour
{
    [SerializeField] MoveAlgorythmGetterBase moveAlgorythmGetter;
    private void Start()
    {
        if (moveAlgorythmGetter == null) return;
        LoopMover[] loopMovers = GetComponentsInChildren<LoopMover>();
        for (int i = 0; i < loopMovers.Length; i++)
        {
            loopMovers[i].MoveData = moveAlgorythmGetter.GetMoveData(i);
            loopMovers[i].StartMove();
        }
    }
}
