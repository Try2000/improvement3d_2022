using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakeScaleAnimation : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] float strength = 1;
    [SerializeField] int vibrato = 10;
    [SerializeField] float randomness = 90;
    private void Awake()
    {
        transform.DOShakeScale(duration, strength, vibrato, randomness).SetLoops(-1);
    }
}
