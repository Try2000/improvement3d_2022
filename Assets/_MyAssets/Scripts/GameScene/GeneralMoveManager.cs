using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GeneralMoverData
{
    public string name;
    public GeneralMover generalMover;
    public MoveData generalMoveData;
    public bool isDefaultActivated = true;
}
public class GeneralMoveManager : MonoBehaviour
{
    [SerializeField] GeneralMoverData[] generalMoverDatas;

    private void Awake()
    {
        Array.ForEach(generalMoverDatas, generalMoverData =>
        {
            generalMoverData.generalMover.MoveData = generalMoverData.generalMoveData;
            generalMoverData.generalMover.gameObject.SetActive(generalMoverData.isDefaultActivated);
        });
    }
    public void PlayMove(string name)
    {
        GeneralMoverData generalMoverData = GetMoverData(name);
        if (generalMoverData == null) return;
        generalMoverData.generalMover.MoveData = generalMoverData.generalMoveData;
        generalMoverData.generalMover.Move();
    }

    GeneralMoverData GetMoverData(string name)
    {
        return Array.Find(generalMoverDatas, generalMoverData => generalMoverData.name == name);
    }
}
