using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class GIFPlayer : MonoBehaviour,IFixedUpdater
{
    static readonly string BASEPATH = "Assets/_MyAssets/ImageFiles/GIF/";
    [SerializeField] string imageName;
    [SerializeField] float deactivateDelay = 4;
    [SerializeField] float gifInterval = 0.2f;
    List<Texture2D> mFrames = new List<Texture2D>();
    bool isPlaying = false;
    Renderer _renderer;
    int mCurFrame = 0;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        PrepareImage();
    }
    private void Start()
    {
        PlayGIF();
    }
    public void PrepareImage()
    {
        byte[] data = File.ReadAllBytes(BASEPATH + imageName + ".gif");

        using (var decoder = new MG.GIF.Decoder(data))
        {
            var img = decoder.NextImage();

            while (img != null)
            {
                Texture2D tex = img.CreateTexture();
                if(tex.width % 4 == 0) tex.Compress(false);
                mFrames.Add(tex);

                img = decoder.NextImage();
            }
        }
    }
    public void PlayGIF()
    {
        isPlaying = true;
    }
    float time;

    public void OnFixedUpdate()
    {
        if (!isPlaying) return;
        time += Time.deltaTime;
        if(time > gifInterval)
        {
            mCurFrame = (mCurFrame >= mFrames.Count -1) ? 0 : mCurFrame + 1;
            _renderer.material.mainTexture = mFrames[mCurFrame];
            time = 0;
        }

    }
}
