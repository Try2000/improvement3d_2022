using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class AutoTilingSetter : MonoBehaviour
{
    [SerializeField] Vector3 baseScale = Vector3.one;
    Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        Material material = _renderer.material;
        material.mainTextureScale = new Vector2(transform.lossyScale.x / baseScale.x, transform.lossyScale.y / baseScale.y);
    }

}
