using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateView : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] float updateFrequency;
    float timer;


    void Start()
    {

    }

    void Update()
    {
        if (timer > updateFrequency)
        {
            float frameRate = 1f / Time.deltaTime;
            text.text = $"FPS : {frameRate.ToString("F1")}";
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
