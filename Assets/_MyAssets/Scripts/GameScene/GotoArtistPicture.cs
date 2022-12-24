using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using TMPro;

public class GotoArtistPicture : MonoBehaviour
{
    [SerializeField] float gotoDelay = 12;
    [SerializeField] CinemachineSmoothPath changetoPath;
    [SerializeField] FOVChangeManager fovManager;
    [SerializeField] Transform nextCameraPos;
    [SerializeField] float duration;
    [SerializeField] Ease easingType = Ease.OutSine;
    [SerializeField] Renderer pictureRenderer;
    [SerializeField] TextMeshPro textMesh;
    [SerializeField] GenreAnimation genreAnimation;
    [SerializeField] float largeTextDuration = 0.5f;
    [SerializeField] Ease largeTextEasingType = Ease.OutSine;
    [SerializeField] Transform easeoutTransform;
    [SerializeField] Ease outEase = Ease.InSine;
    [SerializeField] float outDuration = 0.2f;
    [SerializeField] float NearClipPlane = 15;
   

    private void Awake()
    {
        ShowCaseData showCaseData = genreAnimation.GetShowCaseData();
        textMesh.SetText(showCaseData.showcaseName);
        Texture artistTexture = showCaseData.artistPicture.texture;
        pictureRenderer.material.SetTexture("_MainTex",artistTexture);
        GenreTextData genreTextData = genreAnimation.GetGenreData();
        DOVirtual.DelayedCall(gotoDelay, () =>
        {
            genreTextData.largeTextObject.transform.DOScale(Vector3.zero, largeTextDuration).SetEase(largeTextEasingType);
            DollyCartController.Instance.ChangePath(changetoPath);
            //fovManager.StartAnimation();
            VirtualCameraController.Instance.TmpReleaseFollow();
            CinemachineVirtualCamera cinemachineVirtualCamera = VirtualCameraController.Instance.GetMainVCamera();
            cinemachineVirtualCamera.m_Lens.NearClipPlane = NearClipPlane;
            DOTween.Sequence().Append(cinemachineVirtualCamera.transform.DOMove(nextCameraPos.position, duration).SetEase(easingType))
                              .Append(cinemachineVirtualCamera.transform.DOMove(easeoutTransform.position, outDuration).SetEase(outEase))
                              .Append(cinemachineVirtualCamera.transform.DOMove(nextCameraPos.position, outDuration).SetEase(easingType));

        },false);
    }
}
