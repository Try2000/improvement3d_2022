using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTextSetter : MonoBehaviour
{
    [SerializeField] TextMeshPro[] textMeshPros;
    [SerializeField] string[] texts;

    private void Awake()
    {
        for(int i = 0; i < textMeshPros.Length; i++)
        {
            Random.InitState(i);
            string text = texts[Random.Range(0, texts.Length - 1)];
            textMeshPros[i].SetText(text);
        }
    }
}
