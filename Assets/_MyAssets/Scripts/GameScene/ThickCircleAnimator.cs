using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ThickCircleAnimationData
{
    public float toSize = 10;
    public float toThick = 10;
    public float duration = 0.4f;
    public Ease easingType = Ease.OutQuad;
}
public class ThickCircleAnimator : MonoBehaviour
{
    [SerializeField] ThickableShapeView thickableShape;
    ThickCircleAnimationData thickCircleAnimationData = null;
    public ThickCircleAnimationData ThickCircleAnimationData
    {
        set { thickCircleAnimationData = value; }
    }

    public void AnimateCircle()
    {
        if (thickCircleAnimationData == null) return;
        gameObject.SetActive(true);
        DOTween.To(() => thickableShape.Size, (x) => thickableShape.Size = x, thickCircleAnimationData.toSize, thickCircleAnimationData.duration).SetEase(thickCircleAnimationData.easingType);
        DOTween.To(() => thickableShape.Thickness, (x) => thickableShape.Thickness = x, thickCircleAnimationData.toThick, thickCircleAnimationData.duration).SetEase(thickCircleAnimationData.easingType);
    }
}
