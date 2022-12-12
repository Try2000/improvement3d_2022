using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HandImageForAds : SingletonMonoBehaviour<HandImageForAds>
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image handImage;
    [SerializeField] Sprite upFingerSprite;
    [SerializeField] Sprite downFingerSprite;
    [Multiline(5)][SerializeField] string doc;

    void Start()
    {
        gameObject.SetActive(false);
        handImage.sprite = upFingerSprite;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Switch();
        }

        if (Input.GetMouseButton(0))
        {
            handImage.sprite = downFingerSprite;
        }
        else
        {
            handImage.sprite = upFingerSprite;
        }

        rectTransform.position = Input.mousePosition;
    }

    void Switch()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
        handImage.SetAlpha(0);
        handImage.DOFade(1, 0.1f);
    }

    void Hide()
    {
        handImage.DOFade(0, 0.1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

