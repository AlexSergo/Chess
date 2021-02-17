using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        FindAvailableCells();
    }

    private void FindAvailableCells()
    {

        List<Vector3> possibleCell = new List<Vector3>();
        possibleCell.Add(new Vector3(transform.position.x + 2, transform.position.y));
        possibleCell.Add(new Vector3(transform.position.x - 2, transform.position.y));
        possibleCell.Add(new Vector3(transform.position.x, transform.position.y + 2));
        possibleCell.Add(new Vector3(transform.position.x, transform.position.y - 2));
        possibleCell.Add(new Vector3(transform.position.x + 2, transform.position.y + 2));
        possibleCell.Add(new Vector3(transform.position.x - 2, transform.position.y - 2));
        possibleCell.Add(new Vector3(transform.position.x - 2, transform.position.y + 2));
        possibleCell.Add(new Vector3(transform.position.x + 2, transform.position.y - 2));

        foreach(var cellPosition in possibleCell)
        {
            var cell = Cells.GetCell(cellPosition);
            if (cell != null)
                if (cell.GetComponent<Cell>().IsFree() || cell.GetComponent<Cell>().Figure.Color != Color)
                    _availableCells.Add(cell.GetComponent<Cell>());
        }

    }
}
