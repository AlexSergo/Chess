using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class King : Figure
{
    public bool IsCheck { get; private set; } = false;
    private Cell _myCell;

    private void Start()
    {
        _myCell = transform.parent.GetComponent<Cell>();
    }

    private void OnMouseUp()
    {
        if (BrokeUp())
        {
            _myCell.SetLight(UnityEngine.Color.white);
            _myCell = transform.parent.GetComponent<Cell>();
        }
    }

    public bool Checkmate()
    {
        IsCheck = false;
        FindCells();
        if (IsCheck)
            _myCell.SetLight(UnityEngine.Color.red);
        else
            _myCell.SetLight(UnityEngine.Color.white);
        return IsCheck;
    }

    private void FindRookCells()
    {
        List<Cell> cells = new List<Cell>();
        SearchWay.RookCells(transform, cells);
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].transform.childCount > 0)
            {
                if (cells[i].transform.GetChild(0).GetComponent<Rook>() != null || cells[i].transform.GetChild(0).GetComponent<Queen>() != null)
                    IsCheck = true;
            }
        }
    }

    private void FindBishopCells()
    {
        List<Cell> cells = new List<Cell>();
        SearchWay.BishopCells(transform, cells);
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].transform.childCount > 0)
            {
                if (cells[i].transform.GetChild(0).GetComponent<Queen>() != null || cells[i].transform.GetChild(0).GetComponent<Bishop>() != null)
                    IsCheck = true;
            }
        }
    }

    private void FindKnightCells()
    {
        List<Cell> cells = new List<Cell>();
        SearchWay.KnightCells(transform, cells);
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].transform.childCount > 0)
            {
                if (cells[i].transform.GetChild(0).GetComponent<Knight>() != null)
                    IsCheck = true;
            }
        }
    }

    private void FindPawnCells()
    {
        if (Color == FigureColor.Black)
            PawnCheck(2);
        else
            PawnCheck(-2);
    }

    private void PawnCheck(int y)
    {
        Cell cell1 = Cells.GetCell(new Vector3(_myCell.Position.x + 2, _myCell.Position.y + y)).GetComponent<Cell>();
        Cell cell2 = Cells.GetCell(new Vector3(_myCell.Position.x - 2, _myCell.Position.y + y)).GetComponent<Cell>();
        if ((!cell1.IsFree() && cell1.Figure.Color != Color) || (!cell2.IsFree() && cell2.Figure.Color != Color))
            IsCheck = true;
    }

    private void FindCells()
    {
        FindKnightCells();
        FindBishopCells();
        FindRookCells();
        FindPawnCells();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        if (!IsMyCurrentMove()) return;
        FindAvailableCells();
        SetLight();
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
        TryPreventCheck();

    }
}
