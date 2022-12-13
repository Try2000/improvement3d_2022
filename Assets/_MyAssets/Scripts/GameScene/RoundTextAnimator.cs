using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntw.CurvedTextMeshPro;
using DG.Tweening;
[System.Serializable]
public class RoundTextAnimationData
{
    public float fromAngle = 0;
    public float toAngle = 90;
    public float duration = 1;
    public Ease easingType = Ease.OutSine;
    public float fromAngularOffset = 0;
    public float toAngularOffset = 180;
}
public class RoundTextAnimator : MonoBehaviour
{
    RoundTextAnimationData roundTextAnimationData;
    public RoundTextAnimationData RoundTextAnimationData
    {
        set { roundTextAnimationData = value; }
    }
    TextProOnACircle  textProOnACircle;

    public void AnimateRoundText()
    {
        gameObject.SetActive(true);
        textProOnACircle = GetComponent<TextProOnACircle>();
        textProOnACircle.m_arcDegrees = roundTextAnimationData.fromAngle;
        textProOnACircle.m_angularOffset = roundTextAnimationData.fromAngularOffset;
        DOTween.To(() => textProOnACircle.m_arcDegrees, (x) => textProOnACircle.m_arcDegrees = x, roundTextAnimationData.toAngle, roundTextAnimationData.duration).SetEase(roundTextAnimationData.easingType).SetLink(gameObject);
        DOTween.To(() => textProOnACircle.m_angularOffset, (x) => textProOnACircle.m_angularOffset = x, roundTextAnimationData.toAngularOffset, roundTextAnimationData.duration).SetEase(roundTextAnimationData.easingType).SetLink(gameObject);
    }
}
