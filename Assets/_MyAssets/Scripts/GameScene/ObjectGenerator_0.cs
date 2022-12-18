using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
#endif
/// <summary>
/// SinGenerator
/// </summary>
public enum GenerateMode
{
    Sin,
    PerlinNoize,
    Straight
}
public class ObjectGenerator_0 : MonoBehaviour
{
    [SerializeField] public Transform parentTransform;
    [SerializeField] public GameObject prefab;
    [SerializeField] public GameObject lastObject;
    [SerializeField] public Vector3Int num;
    [SerializeField] public float widthAmp;
    [SerializeField] public float phaseMul = 2;
    [SerializeField] public Vector3 distance;
    [SerializeField] public GenerateMode generateMode = GenerateMode.Sin;
    [SerializeField] public float perlinseedY = 0;
#if UNITY_EDITOR
    [CustomEditor(typeof(ObjectGenerator_0), true)]
    [CanEditMultipleObjects]
    public class ObjectGeneratorEditor_0 : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ObjectGenerator_0 objectGenerator_0 = (ObjectGenerator_0)target;
            if (GUILayout.Button("Generate"))
            {
                List<GameObject> collidableFloors = new List<GameObject>();
                
                for (int ix = 0; ix < objectGenerator_0.num.x; ix++)
                {
                    for (int iz = 0; iz < objectGenerator_0.num.z; iz++)
                    {
                        for (int iy = 0; iy < objectGenerator_0.num.y; iy++)
                        {
                            GameObject obj = PrefabUtility.InstantiatePrefab(objectGenerator_0.prefab) as GameObject;
                            obj.transform.SetParent(objectGenerator_0.parentTransform);
                            obj.transform.position = GetPosition(objectGenerator_0, new Vector3Int(ix, iy, iz));
                            Undo.RegisterCreatedObjectUndo(obj, "Create New GameObject");
                        }
                    }
                }
                //GameObject lastobj = PrefabUtility.InstantiatePrefab(objectGenerator_0.lastObject) as GameObject;
                //lastobj.transform.SetParent(objectGenerator_0.parentTransform);

                //lastobj.transform.position = GetPosition(objectGenerator_0, objectGenerator_0.num);

                EditorUtility.SetDirty(target);
            }
            if (GUILayout.Button("Delete"))
            {
                for (int i = objectGenerator_0.parentTransform.childCount - 1; i >= 0; i--)
                {
                    Object.DestroyImmediate(objectGenerator_0.parentTransform.GetChild(i).gameObject);
                }

                EditorUtility.SetDirty(target);
            }
        }
        Vector3 GetPosition(ObjectGenerator_0 objectGenerator_0, Vector3Int num)
        {
            switch (objectGenerator_0.generateMode)
            {
                case GenerateMode.Sin:
                    return objectGenerator_0.parentTransform.position + objectGenerator_0.distance.z * num.z * (objectGenerator_0.parentTransform.rotation * objectGenerator_0.parentTransform.forward) + objectGenerator_0.widthAmp * new Vector3(Mathf.Sin(((float)num.z / (float)objectGenerator_0.num.z) * objectGenerator_0.phaseMul), 0, 0);
                case GenerateMode.PerlinNoize:
                    return objectGenerator_0.parentTransform.position + objectGenerator_0.distance.z * num.z * (objectGenerator_0.parentTransform.rotation * objectGenerator_0.parentTransform.forward) + objectGenerator_0.widthAmp * new Vector3(Mathf.PerlinNoise((float)num.z, objectGenerator_0.perlinseedY) - 0.5f, 0, 0);
                case GenerateMode.Straight:
                    return objectGenerator_0.parentTransform.position + new Vector3(objectGenerator_0.distance.x * num.x,objectGenerator_0.distance.y * num.y, objectGenerator_0.distance.z * num.z) ;
            }
            return Vector3.zero;
        }
    }
#endif

}
