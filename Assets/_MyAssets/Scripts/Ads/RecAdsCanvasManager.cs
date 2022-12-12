using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecAdsCanvasManager : MonoBehaviour
{

    [Multiline(5)][SerializeField] string doc;
    [Header("キーを反応させたくない時にチェックを外す")][SerializeField] bool enableKey;

    void Start()
    {
        if (!Application.isEditor) gameObject.SetActive(false);
    }

    void Update()
    {
        if (!enableKey) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            FailedImageForAds.Instance?.Show();
        }

        HandImageForAds.Instance?.OnUpdate();
    }

}
