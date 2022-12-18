using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OneDirectionMoveData
{
    public Vector3 moveSpeed;
}
public enum MoveState
{
    Idle,
    Moving
}

public class OneDirectionMover : MonoBehaviour
{
    [SerializeField] OneDirectionMoveData defaultData;
    OneDirectionMoveData directionalMoveData;
    MoveState moveState = MoveState.Idle;
    public UnityEvent onMoveStart;
    public OneDirectionMoveData DirectionalMoveData
    {
        set { directionalMoveData = value; }
    }
    public void ChangeState(MoveState moveState)
    {
        this.moveState = moveState;
        if(this.moveState == MoveState.Moving && onMoveStart != null) onMoveStart.Invoke();
    }
    private void Awake()
    {
        directionalMoveData = defaultData;
    }

    private void FixedUpdate()
    {
        switch (moveState)
        {
            case MoveState.Moving:
                
                transform.position += directionalMoveData.moveSpeed * Time.deltaTime;
                break;
        }
    }
}
