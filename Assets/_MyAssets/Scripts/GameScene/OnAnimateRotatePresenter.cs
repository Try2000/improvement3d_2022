using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RotateAnimator))]
[RequireComponent(typeof(ThickCircleAnimator))]
public class OnAnimateRotatePresenter : MonoBehaviour
{
    private void Awake()
    {
        RotateAnimator rotateAnimator = GetComponent<RotateAnimator>();
        ThickCircleAnimator thickCircleAnimator = GetComponent<ThickCircleAnimator>();
        thickCircleAnimator.onAnimate += rotateAnimator.RotateAnimate;
    }
}
