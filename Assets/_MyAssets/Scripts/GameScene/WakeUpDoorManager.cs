using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
[System.Serializable]
public class WakeUpCubeData
{
    public Renderer[] renderers;
    public Color color;
    public float delay = 0.5f;
    public float duration = 0.4f;
}
public class WakeUpDoorManager : MonoBehaviour
{
    [SerializeField] WakeUpCubeData[] wakeUpCubeDatas;
    [SerializeField] Ease colorEasingType = Ease.OutQuad;
    [SerializeField] Vector3 wavingAmount;
    [SerializeField] float wavingDuration = 0.4f;
    [SerializeField] Ease wavingEaseType = Ease.InOutSine;
    [SerializeField] Vector3 centerPos;
    [SerializeField] float movingDelay = 0.5f;
    public void PlayWakeUp()
    {
        StartCoroutine(StartWakeUpAnimationDoor());
    }

    IEnumerator StartWakeUpAnimationDoor()
    {
        for(int i = 0; i< wakeUpCubeDatas.Length; i++)
        {
            yield return new WaitForSeconds(wakeUpCubeDatas[i].delay);
           
            Array.ForEach(wakeUpCubeDatas[i].renderers, _renderer =>
            {
              
                _renderer.material.DOColor(wakeUpCubeDatas[i].color, "_LineColor", wakeUpCubeDatas[i].duration).SetEase(colorEasingType);

                
            });
        }
        yield return new WaitForSeconds(movingDelay);
        for(int i = 0; i < wakeUpCubeDatas.Length; i++)
        {
            Array.ForEach(wakeUpCubeDatas[i].renderers, _renderer =>
            {
                bool isLeft = (centerPos.x - _renderer.transform.position.x) < 0;
                _renderer.transform.DOMove((isLeft) ? -wavingAmount : wavingAmount, wavingDuration).SetRelative().SetEase(Ease.OutQuart);

            });
        }
    }
}
