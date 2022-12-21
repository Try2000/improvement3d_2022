using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshPro))]
public class TestTEXT : MonoBehaviour
{
    [SerializeField] TMP_FontAsset _FontAsset;
    [SerializeField] string characters;
    [SerializeField] int samplingNum = 3;
    [SerializeField] Vector3 leftPos;
    [SerializeField] Vector3 charDistance;
    [SerializeField] float size = 80;
    [SerializeField] float thresholdAlpha;
    [SerializeField] GameObject prefab;
    TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
       
        List<TMP_Character> tMP_Characters = new List<TMP_Character>();
        Texture2D tex = _FontAsset.atlasTexture;
        
        int count = 0;
         
        for (int i = 0; i < characters.Length; i++)
        {
            List<Vector3> vertices = new List<Vector3>();
            Vector3 basePos = leftPos + charDistance * i;
            TMP_Character tmp =  _FontAsset.characterTable.Find(character=>character.unicode == characters[i]);
            for (int iy = 0; iy <  tmp.glyph.glyphRect.height; iy++)
            {
                for (int ix = 0; ix < tmp.glyph.glyphRect.width; ix++)
                {
                    count++;
                    if(count > samplingNum)
                    {
                        count = 0;
                        if(  tex.GetPixel(tmp.glyph.glyphRect.x + ix, tmp.glyph.glyphRect.y + iy).a > thresholdAlpha ) vertices.Add(basePos + new Vector3(ix,iy,0)*size);
                    }
                }
            }
            ColorAllPoint(vertices.ToArray());
        }

    }

    public void ColorAllPoint(Vector3[] points)
    {
        for(int i = 0; i  < points.Length-1;i++)
        {
            Debug.DrawLine(points[i], points[i + 1], Color.red, 20);
        }
    }

}
