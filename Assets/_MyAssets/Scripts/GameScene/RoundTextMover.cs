using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ntw.CurvedTextMeshPro;
using DG.Tweening;

public class RoundTextMover : MonoBehaviour
{
    [SerializeField] float fromAngle = 0;
    [SerializeField] float toAngle = 90;
    [SerializeField] float duration = 1;
    [SerializeField] Ease easingType = Ease.OutSine;
    [SerializeField] float fromAngularOffset = 0;
    [SerializeField] float toAngularOffset = 180;
    TextProOnACircle  textProOnACircle;
    private void Awake()
    {
        textProOnACircle = GetComponent<TextProOnACircle>();
        textProOnACircle.m_arcDegrees = fromAngle;
        textProOnACircle.m_angularOffset = fromAngularOffset;
        DOTween.To(() => textProOnACircle.m_arcDegrees, (x) => textProOnACircle.m_arcDegrees = x, toAngle, duration).SetEase(easingType).SetLink(gameObject);
        DOTween.To(() => textProOnACircle.m_angularOffset, (x) => textProOnACircle.m_angularOffset = x, toAngularOffset, duration).SetEase(easingType).SetLink(gameObject);
    }
}
