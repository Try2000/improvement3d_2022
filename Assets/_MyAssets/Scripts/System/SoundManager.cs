using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        i = this;
    }

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        var soundManagerPrefab = Resources.Load("System/SoundManager");
        var soundManager = Instantiate(soundManagerPrefab, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(soundManager);
        Debug.Log("Created SoundManager");
    }

    public void PlayOneShot(int resourceIndex)
    {
        if (SaveData.i.isOffSE) { return; }
        if (SoundResourceSO.Instance.resources.Length - 1 < resourceIndex) { return; }
        AudioClip clip = SoundResourceSO.Instance.resources[resourceIndex].audioClip;
        if (clip == null) { return; }
        audioSource.PlayOneShot(clip);
    }

}
