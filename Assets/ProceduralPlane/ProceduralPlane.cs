using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPlane : MonoBehaviour
{
    private MeshFilter _meshFilter;

    public int planeSize = 10;
    public float noiseScale = 1;
    public float noiseAmplitude = 1;

    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.sharedMesh = CreateMesh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_meshFilter.sharedMesh);
            _meshFilter.sharedMesh = CreateMesh();
        }
    }

    private Mesh CreateMesh()
    {
        var mesh = new Mesh();
        mesh.name = "Plane";

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        for (int x = 0; x < planeSize; x++)
        {
            for (int z = 0; z < planeSize; z++)
            {
                var height = Mathf.PerlinNoise((float) x / (planeSize - 1) * noiseScale, (float) z / (planeSize - 1) * noiseScale) * noiseAmplitude;
                var vertex = new Vector3(x, height, z);
                vertices.Add(vertex);
            }
        }
        
        for (int x = 0; x < planeSize - 1; x++)
        {
            for (int z = 0; z < planeSize - 1; z++)
            {
                DrawQuad(x, z, triangles);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    private void DrawQuad(int x, int z, List<int> triangles)
    {
        int i1 = z + (x * planeSize);
        int i2 = i1 + 1;
        int i3 = i2 + planeSize;
        int i4 = i3 - 1;

        triangles.Add(i1);
        triangles.Add(i2);
        triangles.Add(i3);

        triangles.Add(i1);
        triangles.Add(i3);
        triangles.Add(i4);
    }

    // private void OnDrawGizmos()
    // {
    //     if (_meshFilter && _meshFilter.sharedMesh)
    //     {
    //         for (int i = 0; i < _meshFilter.sharedMesh.vertices.Length; i++)
    //         {
    //             Gizmos.DrawSphere(_meshFilter.sharedMesh.vertices[i], 0.1f);
    //         }
    //     }
    // }
}