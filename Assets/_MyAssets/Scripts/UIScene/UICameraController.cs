using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem confettiL;
    [SerializeField] ParticleSystem confettiR;
    public Camera cam;
    public static UICameraController i;

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        // https://qiita.com/ptkyoku/items/5602733ba9cff0ccd54d
        cam.cullingMask = 1 << LayerMask.NameToLayer("Confetti");

        if (confettiL)
        {
            confettiL.gameObject.layer = LayerMask.NameToLayer("Confetti");
            foreach (Transform item in confettiL.transform)
            {
                item.gameObject.layer = LayerMask.NameToLayer("Confetti");
            }
        }

        if (confettiR)
        {
            confettiR.gameObject.layer = LayerMask.NameToLayer("Confetti");
            foreach (Transform item in confettiR.transform)
            {
                item.gameObject.layer = LayerMask.NameToLayer("Confetti");
            }
        }

    }

    public void PlayConfetti()
    {
        ShowConfetti(show: true);
        if (confettiL) confettiL.Play();
        if (confettiR) confettiR.Play();
    }

    public void ShowConfetti(bool show)
    {
        show = DebugSettingSO.Instance.hideConfetti ? false : show;
        if (confettiL) confettiL.gameObject.SetActive(show);
        if (confettiR) confettiR.gameObject.SetActive(show);
    }
}