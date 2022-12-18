using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RotateAnimationData
{
    public Vector3 toRotateAngle;
    public float duration;
    public Ease easingType = Ease.Linear;
}

public class RotateAnimator : MonoBehaviour
{
    [SerializeField] RotateAnimationData defaultAnimationData;
    RotateAnimationData animationData;
    private void Awake()
    {
        animationData = defaultAnimationData;
    }
    public void RotateAnimate()
    {
        transform.DORotate(animationData.toRotateAngle, animationData.duration,RotateMode.FastBeyond360).SetEase(animationData.easingType).SetRelative();
    }
}
