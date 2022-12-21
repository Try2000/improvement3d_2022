using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenreAnimation : MonoBehaviour
{
    [SerializeField] ShowCaseDataSO showCaseDataSO;

    private void Awake()
    {
        TextMeshPro[] textMeshPros = transform.GetComponentsInChildren<TextMeshPro>();
        for(int i = 0; i < textMeshPros.Length; i++)
        {
            textMeshPros[i].SetText(showCaseDataSO.ShowCaseData.names[Random.Range(0, showCaseDataSO.ShowCaseData.names.Length - 1)]);
            Random.InitState(i);
        }
    }
}
