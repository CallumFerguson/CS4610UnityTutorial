using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerate : MonoBehaviour
{
    public Material material;
    
    public void GenerateMaze(bool[,] mazeArray)
    {
        var meshObject = new GameObject();
        meshObject.name = "Mesh Object";
        meshObject.transform.parent = transform;

        var meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;

        var meshFilter = meshObject.AddComponent<MeshFilter>();

        var mesh = new Mesh();
        mesh.name = "Maze Mesh";
        meshFilter.sharedMesh = mesh;

        var vertices = new List<Vector3>();
        var uv = new List<Vector2>();
        var triangles = new List<int>();
        
        var mazeSize = mazeArray.GetLength(0);

        var hasHitBox = new bool[mazeSize, mazeSize];

        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                if (mazeArray[x, y] && !hasHitBox[x, y])
                {
                    var size = FindLargestBlock(mazeArray, hasHitBox, x, y);
                    AddBlock(vertices, uv, triangles, x, y, size.width, size.height);
                }
            }
        }
        
        AddBlock(vertices, uv, triangles, 0, 0, 1, 1);
        AddBlock(vertices, uv, triangles, 1, 0, 3, 8);

        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    private (int width, int height) FindLargestBlock(bool[,] mazeArray, bool[,] hasHitBox, int x, int y)
    {
        var mazeSize = mazeArray.GetLength(0);

        int xStart = x;
        int yStart = y;

        while (y < mazeSize - 1)
        {
            if (mazeArray[x, y + 1] && !hasHitBox[x, y + 1])
            {
                y++;
            }
            else
            {
                break;
            }
        }

        while (x < mazeSize - 1)
        {
            var canExpand = true;
            for (int i = yStart; i <= y; i++)
            {
                if (!(mazeArray[x + 1, i] && !hasHitBox[x + 1, i]))
                {
                    canExpand = false;
                    break;
                }
            }

            if (!canExpand)
            {
                break;
            }

            x++;
        }

        for (int i = xStart; i <= x; i++)
        {
            for (int n = yStart; n <= y; n++)
            {
                hasHitBox[i, n] = true;
            }
        }

        int width = x - xStart + 1;
        int height = y - yStart + 1;
        return (width, height);
    }

    private void AddBlock(List<Vector3> vertices, List<Vector2> uv, List<int> triangles, int x, int y, int width, int height)
    {
        var vertexOffset = vertices.Count;
        
        vertices.Add(new Vector3(x, y));
        vertices.Add(new Vector3(x + width, y));
        vertices.Add(new Vector3(x + width, y + height));
        vertices.Add(new Vector3(x, y + height));
        
        uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(width, 0));
        uv.Add(new Vector2(width, height));
        uv.Add(new Vector2(0, height));
        
        triangles.Add(0 + vertexOffset);
        triangles.Add(2 + vertexOffset);
        triangles.Add(1 + vertexOffset);
        
        triangles.Add(0 + vertexOffset);
        triangles.Add(3 + vertexOffset);
        triangles.Add(2 + vertexOffset);

        var boxObject = new GameObject();
        boxObject.transform.parent = transform;
        boxObject.name = "box collider";
        boxObject.transform.position = new Vector3(x + width / 2f, y + height / 2f, 0);

        var boxCollider = boxObject.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(width, height);
    }
}