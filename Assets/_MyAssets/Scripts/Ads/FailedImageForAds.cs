using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FailedImageForAds : SingletonMonoBehaviour<FailedImageForAds>
{
    [SerializeField] Image failedImage;
    void Start()
    {
        failedImage.gameObject.SetActive(false);
    }

    public void Show()
    {
        if (!Application.isEditor) return;
        failedImage.gameObject.SetActive(true);
        failedImage.SetAlpha(0);
        failedImage.transform.localScale = Vector3.one * 2.0f;
        failedImage.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear);
        failedImage.DOFade(1, 0.2f);
    }
}
