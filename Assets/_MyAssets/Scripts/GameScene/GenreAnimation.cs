using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

[System.Serializable]
public class GenreTextData
{
    public Genre genre;
    public GameObject genreParent;
    public GameObject largeTextObject;
}

public class GenreAnimation : MonoBehaviour
{
    [SerializeField] ShowCaseDataSO showCaseDataSO;
    [SerializeField] float baseDuration = 0.5f;
    [SerializeField] float randomMax = 1;
    [SerializeField] float randomMin = -1;
    [SerializeField] float largeTextDelay = 5;
    [SerializeField] GenreTextData[] genreTextDatas;
    [SerializeField] Transform basePosTransform;
    [SerializeField] Transform[] paths;

    private void Awake()
    {
        foreach(GenreTextData genreTextData in genreTextDatas)
        {
            genreTextData.largeTextObject.SetActive(false);
            genreTextData.genreParent.SetActive(genreTextData.genre == showCaseDataSO.ShowCaseData.genre);
            if(genreTextData.genre == showCaseDataSO.ShowCaseData.genre)
            {
                Vector3 diff = basePosTransform.position - genreTextData.genreParent.transform.position;
                Array.ForEach(paths, path =>
                {
                    path.position -= new Vector3(0,0,diff.z);
                });
            }
        }
        TextMeshPro[] textMeshPros = transform.GetComponentsInChildren<TextMeshPro>();
        for(int i = 0; i < textMeshPros.Length; i++)
        {
            float duration = baseDuration + UnityEngine.Random.Range(randomMin, randomMax);
            textMeshPros[i].DOText(showCaseDataSO.ShowCaseData.names[UnityEngine.Random.Range(0, showCaseDataSO.ShowCaseData.names.Length - 1)], duration, false, ScrambleMode.Uppercase).SetEase(Ease.Linear);
            UnityEngine.Random.InitState(i);
        }
        DOVirtual.DelayedCall(largeTextDelay, () =>
        {
            ChangeTexts();
        });
    }

    public void ChangeTexts()
    {
        GenreTextData genreTextData = Array.Find(genreTextDatas, genreTextData => genreTextData.genre == showCaseDataSO.ShowCaseData.genre);
        if (genreTextData == null) return;
        genreTextData.genreParent.SetActive(false);
        genreTextData.largeTextObject.SetActive(true);
    }
}
