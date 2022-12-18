using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
[RequireComponent(typeof(TextMeshPro))]
public class TextMeshProEffect_0 : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    TextMeshPro tmpro;
    MoveState moveState;
    private void Awake()
    {
        tmpro = GetComponent<TextMeshPro>();
        
    }
    public void StartGlich()
    {
        ChangeState(MoveState.Moving);
    }
    public void ChangeState(MoveState moveState)
    {
        this.moveState = moveState;
        switch (moveState)
        {
            case MoveState.Idle:
                break;
            case MoveState.Moving:
                string _text = tmpro.text;
                tmpro.DOText(_text, duration, false, ScrambleMode.Uppercase).SetEase(Ease.Linear);
                break;
        }
    }
}
