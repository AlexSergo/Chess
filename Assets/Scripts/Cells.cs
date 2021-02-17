using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cells
{
    public static Cell[,] AllCells;
    private const int _countCells = 8;

    public static void InitializationCells(GameObject[,] map)
    {
        AllCells = new Cell[_countCells, _countCells];
        for (int i = 0; i < _countCells; i++)
            for (int j = 0; j < _countCells; j++)
                AllCells[i, j] = map[i,j].GetComponent<Cell>();

    }

    public static Transform GetCell(Vector3 position)
    {
        position = new Vector3((int)Math.Round(position.x), (int)Math.Round(position.y));
        try
        {
            return AllCells[(int)position.y / 2, (int)position.x / 2].transform;
        }
        catch
        {
            return null;
        }
    }
}
