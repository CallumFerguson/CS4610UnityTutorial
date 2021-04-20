using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanTest : MonoBehaviour
{
    public Transform testObject;
    public MeshRenderer meshRenderer;

    private Material _material;
    
    // Start is called before the first frame update
    void Start()
    {
        _material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        var localPosition = transform.InverseTransformPoint(testObject.position);
        var onTop = localPosition.y > 0;

        if (onTop)
        {
            _material.SetColor("_Color", Color.blue);
        }
        else
        {
            _material.SetColor("_Color", Color.red);
        }
    }
}