using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        for (int i = -2; i <= 2; i++)
        {
            if (i == 0) i++;

            if (i == -2 || i == 2)
                FindAvailableCells(i * 2, 2);
            else
                FindAvailableCells(i * 2, 4);
        }
    }

    private void FindAvailableCells(int i, int j)
    {
        List<Vector3> possibleCells = new List<Vector3>();
        possibleCells.Add(new Vector3(transform.position.x + i, transform.position.y - j));
        possibleCells.Add(new Vector3(transform.position.x + i, transform.position.y + j));
        foreach (var possibleCell in possibleCells)
        {
            var cell = Cells.GetCell(possibleCell);
            try
            {
                Cell currentCell = cell.GetComponent<Cell>();
                if ((currentCell.Figure != null && currentCell.Figure.Color != Color) || currentCell.IsFree())
                    _availableCells.Add(cell.GetComponent<Cell>());
            }
            catch { }
        }
    }
}
