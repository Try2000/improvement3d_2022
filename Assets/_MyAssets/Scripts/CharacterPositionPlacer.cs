using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
#endif

public class CharacterPositionPlacer : MonoBehaviour
{
    [SerializeField] public TMP_FontAsset _FontAsset;
    [SerializeField] public string characters;
    [SerializeField] public float size = 20;
    [SerializeField] public GameObject prefab;
    [SerializeField] public int samplingNum;
    [SerializeField] public int placeDensity = 5;
    [SerializeField] public float thresholdAlpha;
    [SerializeField] public Transform baseTransform;
    [SerializeField] public Vector3 charaDistance;
    [SerializeField] public Transform parent;
    [SerializeField] public Vector3 randomMax;
    [SerializeField] public Vector3 randomMin;
#if UNITY_EDITOR
    [CustomEditor(typeof(CharacterPositionPlacer), true)]
    [CanEditMultipleObjects]
    public class CharacterPositionPlacer_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CharacterPositionPlacer characterPositionPlacer = (CharacterPositionPlacer)target;
            if (GUILayout.Button("Generate"))
            {
                for (int i = 0; i < characterPositionPlacer.characters.Length; i++)
                {
                    GameObject parentObj = new GameObject();
                    parentObj.transform.SetParent(characterPositionPlacer.parent);
                    parentObj.name = characterPositionPlacer.characters[i].ToString();
                    Vector3[] vertices = GetCharacterVertices(characterPositionPlacer, characterPositionPlacer.characters[i], characterPositionPlacer.baseTransform.position + characterPositionPlacer.charaDistance * i);
                    int count = 0;
                    for(int l = 0; l < vertices.Length; l++)
                    {
                        count++;
                        if(count > characterPositionPlacer.placeDensity)
                        {
                            count = 0;
                            Instantiate(characterPositionPlacer.prefab, vertices[l] + new Vector3(Random.Range(characterPositionPlacer.randomMin.x, characterPositionPlacer.randomMax.x), Random.Range(characterPositionPlacer.randomMin.y, characterPositionPlacer.randomMax.y), Random.Range(characterPositionPlacer.randomMin.z, characterPositionPlacer.randomMax.z)), characterPositionPlacer.prefab.transform.rotation, parentObj.transform);
                        }
                    }
                }

                EditorUtility.SetDirty(target);
            }
            if (GUILayout.Button("Delete"))
            {
                for (int i = characterPositionPlacer.parent.childCount - 1; i >= 0; i--)
                {
                    Object.DestroyImmediate(characterPositionPlacer.parent.GetChild(i).gameObject);
                }

                EditorUtility.SetDirty(target);
            }
        }

        public Vector3[] GetCharacterVertices(CharacterPositionPlacer characterPositionPlacer, char chara, Vector3 basePos)
        {
            Texture2D tex = characterPositionPlacer._FontAsset.atlasTexture;
            List<Vector3> vertices = new List<Vector3>();
            TMP_Character tmp = characterPositionPlacer._FontAsset.characterTable.Find(character => character.unicode == chara);
            int count = 0;
            for (int iy = 0; iy < tmp.glyph.glyphRect.height; iy++)
            {
                for (int ix = 0; ix < tmp.glyph.glyphRect.width; ix++)
                {
                    count++;
                    if (count > characterPositionPlacer.samplingNum)
                    {
                        count = 0;
                        if (tex.GetPixel(tmp.glyph.glyphRect.x + ix, tmp.glyph.glyphRect.y + iy).a > characterPositionPlacer.thresholdAlpha) vertices.Add(basePos + new Vector3(ix, iy, 0) * characterPositionPlacer.size);
                    }
                }
            }
            return vertices.ToArray();
        }
    }
#endif

}
