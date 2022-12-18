using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MoveData
{
    public Vector3 moveAmount;
    public float duration = 0.4f;
    public Ease easingType = Ease.OutQuad;
    public float delay = 0;
    public bool isRelative = true;

    public MoveData(Vector3 moveAmount, float duration, Ease easingType, bool isRelative,float delay)
    {
        this.moveAmount = moveAmount;
        this.duration = duration;
        this.easingType = easingType;
        this.isRelative = isRelative;
        this.delay = delay;
    }
}

public class GeneralMover : MonoBehaviour
{
    MoveData moveData = null;
    public MoveData MoveData
    {
        set { moveData = value; }
    }
    public void Move()
    {
        if (moveData.isRelative) { transform.DOMove(moveData.moveAmount, moveData.duration).SetDelay(moveData.delay).SetEase(moveData.easingType).SetRelative(); }
        else { transform.DOMove(moveData.moveAmount, moveData.duration).SetDelay(moveData.delay).SetEase(moveData.easingType); }
    }
}
