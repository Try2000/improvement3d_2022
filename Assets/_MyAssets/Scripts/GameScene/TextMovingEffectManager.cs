using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovingEffectManager : MonoBehaviour
{
    [SerializeField] OneDirectionMover[] oneDirectionMovers;
    [SerializeField] float startDelay = 0.8f;

    private void Awake()
    {
        //Array.ForEach(oneDirectionMovers, oneDirectionMover =>
        //{
        //    oneDirectionMover.gameObject.SetActive(false);
        //});
        DOVirtual.DelayedCall(startDelay, () =>
        {
            Array.ForEach(oneDirectionMovers, oneDirectionMover =>
            {
                oneDirectionMover.gameObject.SetActive(true);
                oneDirectionMover.ChangeState(MoveState.Moving);
            });
        });
        
    }
}
