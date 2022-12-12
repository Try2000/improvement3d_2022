using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyCartController : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cinemachineDollyCart;
    [SerializeField] Ease easingType = Ease.InQuad;
    [SerializeField] float duration = 0.3f;
    public void ChangeSpeed(float speed)
    {
        cinemachineDollyCart.m_Speed = speed;
    }
    public void ChangeSpeedWithEasing(float toSpeed)
    {
        DOTween.To(() => cinemachineDollyCart.m_Speed, (x) => cinemachineDollyCart.m_Speed = x, toSpeed, duration).SetEase(easingType);
    }
}
