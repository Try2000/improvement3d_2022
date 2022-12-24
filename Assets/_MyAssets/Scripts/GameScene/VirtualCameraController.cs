using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public struct VCameraData
{
    public CinemachineVirtualCamera cinemachineVirtual;
    public int defaultPriority;

    public VCameraData(CinemachineVirtualCamera cinemachineVirtualCamera, int defaultPriority)
    {
        this.cinemachineVirtual = cinemachineVirtualCamera;
        this.defaultPriority = defaultPriority;
    }
}
public class VirtualCameraController : SingletonMonoBehaviour<VirtualCameraController>
{
    const int INFINITY = 1000;
    VCameraData[] virtualCameraDatas;
    int mainIndex = 0;
    private void Awake()
    {
        virtualCameraDatas = GetComponentsInChildren<CinemachineVirtualCamera>().Select(cv =>
        {
            return new VCameraData(cv, cv.Priority);
        }).ToArray();
        followTransformStack = new Stack<Transform>();
    }

    public void ChangeCamera(int index)
    {
        mainIndex = index;
        foreach (VCameraData vCameraData in virtualCameraDatas)
        {
            vCameraData.cinemachineVirtual.Priority = vCameraData.defaultPriority;
        }
        virtualCameraDatas[Mathf.Min(virtualCameraDatas.Length - 1, index)].cinemachineVirtual.Priority = INFINITY;
    }

    public VCameraData GetMainVCameraData()
    {
        return (mainIndex < virtualCameraDatas.Length - 1) ? virtualCameraDatas[mainIndex] : virtualCameraDatas[0];
    }

    public CinemachineVirtualCamera GetMainVCamera()
    {
        return (mainIndex < virtualCameraDatas.Length - 1) ? virtualCameraDatas[mainIndex].cinemachineVirtual : virtualCameraDatas[0].cinemachineVirtual;
    }
    Stack<Transform> followTransformStack;

    public void TmpReleaseFollow()
    {
        VCameraData mainData = GetMainVCameraData();
        followTransformStack.Push(mainData.cinemachineVirtual.Follow);
        mainData.cinemachineVirtual.Follow = null;
    }
    public void ResetFollow()
    {
        VCameraData mainData = GetMainVCameraData();
        var tmp = followTransformStack.Pop();
        Debug.Log(tmp);
        mainData.cinemachineVirtual.Follow = tmp;
    }
}
