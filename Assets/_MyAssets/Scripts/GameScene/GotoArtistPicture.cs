using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class GotoArtistPicture : MonoBehaviour
{
    [SerializeField] float gotoDelay = 12;
    [SerializeField] CinemachineSmoothPath changetoPath;
    [SerializeField] FOVChangeManager fovManager;

    private void Awake()
    {
        DOVirtual.DelayedCall(gotoDelay, () =>
        {
            DollyCartController.Instance.ChangePath(changetoPath);
            fovManager.StartAnimation();
        });
    }
}
