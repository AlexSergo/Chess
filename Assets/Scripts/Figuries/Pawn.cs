using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    private const int _step = 2;
    private bool _wasFirstMove = false;

    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        if (!IsMyCurrentMove()) return;
        if (Color == FigureColor.Black)
        {
            Move(_step);
            Kill(_step);
        }
        else
        {
            Move(-_step);
            Kill(-_step);
        }
        TryPreventCheck();
    }

    private void Kill(int step)
    {
        Vector3 cellPosition1 = new Vector3(transform.position.x - step, transform.position.y + step);
        Vector3 cellPosition2 = new Vector3(transform.position.x + step, transform.position.y + step);

        var cell = Cells.GetCell(cellPosition1);
        try
        {
            Cell currentCell = cell.GetComponent<Cell>();
            if (currentCell.Figure != null && cell.GetComponent<Cell>().Figure.Color != Color)
                _availableCells.Add(cell.GetComponent<Cell>());
        }
        catch { }

        cell = Cells.GetCell(cellPosition2);
        try
        {
            Cell currentCell = cell.GetComponent<Cell>();
            if (currentCell.Figure != null && currentCell.Figure.Color != Color)
                _availableCells.Add(currentCell);
        }
        catch { }
    }

    private void Move(int step)
    {
        Vector3 cellPosition = new Vector3(transform.position.x, transform.position.y + step);
        var cell = Cells.GetCell(cellPosition);
        if (cell != null && cell.GetComponent<Cell>().IsFree())
            _availableCells.Add(cell.GetComponent<Cell>());

        if (!_wasFirstMove)
        {
            cellPosition = new Vector3(transform.position.x, transform.position.y + step * 2);
            cell = Cells.GetCell(cellPosition);
            if (cell != null && cell.GetComponent<Cell>().IsFree())
                _availableCells.Add(cell.GetComponent<Cell>());
            _wasFirstMove = true;
        }
    }
}
