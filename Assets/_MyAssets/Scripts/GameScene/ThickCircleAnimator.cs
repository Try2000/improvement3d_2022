using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThickCircleAnimator : MonoBehaviour
{
    [SerializeField] ThickableShapeView thickableShape;
    [SerializeField] int toSize = 10;
    [SerializeField] int toThick = 10;
    [SerializeField] float duration = 0.4f;
    [SerializeField] Ease easingType = Ease.OutQuad;
 
    public void AnimateCircle()
    {
        DOTween.To(() => thickableShape.Size, (x) => thickableShape.Size = x, toSize, duration).SetEase(easingType);
        DOTween.To(() => thickableShape.Thickness, (x) => thickableShape.Thickness = x, toThick, duration).SetEase(easingType);
    }
}
