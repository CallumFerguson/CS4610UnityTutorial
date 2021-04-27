using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeArray : MonoBehaviour
{
    public int mazeSize = 25;
    
    private MazeGenerate _mazeGenerate;

    void Start()
    {
        _mazeGenerate = GetComponent<MazeGenerate>();
        
        GenerateNewMaze();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            GenerateNewMaze();
        }
    }

    private void GenerateNewMaze()
    {
        var mazeArray = GenerateMazeArray();
        _mazeGenerate.GenerateMaze(mazeArray);
    }

    private bool[,] GenerateMazeArray()
    {
        var mazeArray = new bool[mazeSize, mazeSize];

        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                mazeArray[x, y] = Random.value < 0.4f || x == 0 || x == mazeSize - 1 || y == 0 || y == mazeSize - 1;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            mazeArray = CellularAutomatonIteration(mazeArray);
        }

        return mazeArray;
    }

    private bool[,] CellularAutomatonIteration(bool[,] mazeArray)
    {
        var newMazeArray = new bool[mazeSize, mazeSize];
        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                newMazeArray[x, y] = mazeArray[x, y];
            }
        }

        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                int numNeighbors = 0;
                for (int xi = -1; xi <= 1; xi++)
                {
                    for (int yi = -1; yi <= 1; yi++)
                    {
                        var xTest = x + xi;
                        var yTest = y + yi;
                        if (!(xi == 0 && yi == 0) && xTest > 0 && xTest < mazeSize && yTest > 0 && yTest < mazeSize && mazeArray[x + xi, y + yi])
                        {
                            numNeighbors++;
                        }
                    }
                }

                if (mazeArray[x, y])
                {
                    if (numNeighbors < 3 && x != 0 && x != mazeSize - 1 && y != 0 && y != mazeSize - 1)
                    {
                        newMazeArray[x, y] = false;
                    }
                }
                else
                {
                    if (numNeighbors > 4)
                    {
                        newMazeArray[x, y] = true;
                    }
                }
            }
        }

        return newMazeArray;
    }
}