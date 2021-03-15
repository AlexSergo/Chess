using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    public GameObject[] Field;
    public Transform Map; 
    public GameObject[] Figuries;
    private const int CountFigure = 16;
    private const int CountCells = 8;
    private GameObject[,] MapCells = new GameObject[CountCells, CountCells];
    void Start()
    {
        int k = 0;
        for (int i = 0; i < CountCells; i++)
        {
            for (int j = 0; j < CountCells; j++)
            {
                MapCells[i, j] = Instantiate(Field[k]);
                MapCells[i, j].transform.position = new Vector3(j*2, i * 2, 0);
                MapCells[i, j].transform.SetParent(Map);
                MapCells[i, j].transform.name = $"{i}{j}";
                if (k < 1)
                    k++;
                else
                    k = 0;
            }
            if (k < 1)
                k++;
            else
                k = 0;
        }
        k = 1;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < CountFigure; i++)
            {
                if (i < CountFigure / 2)
                {
                    if (k == 1)
                    {
                        GameObject figure = Instantiate(Figuries[0]);
                        figure.transform.SetParent(MapCells[6, i].transform, false);
                        figure.transform.localPosition = new Vector3(0,0,0);
                    }
                    else
                    {
                        GameObject figure = Instantiate(Figuries[k]);
                        figure.transform.SetParent(MapCells[1, i].transform, false);
                        figure.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    if (k <= CountFigure / 2)
                    {
                        GameObject figure = Instantiate(Figuries[k]);
                        figure.transform.SetParent(MapCells[7, i % 8].transform, false);
                        figure.transform.localPosition = new Vector3(0, 0, 0);
                    }
                    else 
                    {
                        if (k == 9) k++;
                        GameObject figure = Instantiate(Figuries[k]);
                        figure.transform.SetParent(MapCells[0, i % 8].transform, false);
                        figure.transform.localPosition = new Vector3(0, 0, 0);
                    }
                    k++;
                }
            }
        }

        Cells.InitializationCells(MapCells);
        Game.RotateCamera();
    }
}
