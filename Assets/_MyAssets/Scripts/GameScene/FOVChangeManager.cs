using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class FOVChangeManager : MonoBehaviour
{
    [SerializeField] float toFOV = 60;
    [SerializeField] float duration = 0.4f;
    [SerializeField] Ease easingType = Ease.InQuint;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public void StartAnimation()
    {
        DOTween.To(() => virtualCamera.m_Lens.FieldOfView, (tmp) => virtualCamera.m_Lens.FieldOfView = tmp, toFOV, duration).SetEase(easingType);
    }
}
