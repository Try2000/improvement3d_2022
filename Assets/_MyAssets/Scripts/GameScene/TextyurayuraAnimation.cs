using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class TextyurayuraAnimation : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] float heightMax = 20;
    [SerializeField] Ease easingType = Ease.OutFlash;
    [SerializeField] float delay = 0.4f;
    private DOTweenTMPAnimator tmpAnimator;
    private void Start()
    {
        tmpAnimator = new DOTweenTMPAnimator(textMeshPro);
        Play(duration);
    }
    public void Play(float duration)
    {

        const float EACH_DELAY_RATIO = 0.01f;
        var eachDelay = duration * EACH_DELAY_RATIO;
        var eachDuration = duration - eachDelay;
        var charCount = tmpAnimator.textInfo.characterCount;

        for (var i = 0; i < charCount; i++)
        {
            DOTween.Sequence()
                .Append(tmpAnimator.DOOffsetChar(i, Vector3.up * heightMax, duration).SetEase(easingType))
                .SetDelay(delay * i)
                .SetLoops(-1,LoopType.Yoyo);
        }
    }

}
