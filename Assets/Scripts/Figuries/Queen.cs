using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        FindDiagonalCells(transform, _availableCells);
        FindLineCells(transform, _availableCells);
    }

    protected void FindLineCells(Transform transform, List<Cell> availableCells)
    {
        Line(transform, availableCells, "x", -1, -8);
        Line(transform, availableCells, "x", 1, 8);
        Line(transform, availableCells, "y", -1, -8);
        Line(transform, availableCells, "y", 1, 8);
    }

    protected void FindDiagonalCells(Transform transform, List<Cell> availableCells)
    {
        Diagonal(transform, availableCells, 1, 8, 1, 8);
        Diagonal(transform, availableCells, -1, -8, -1, -8);
        Diagonal(transform, availableCells, 1, 8, -1, -8);
        Diagonal(transform, availableCells, -1, -8, 1, 8);
    }

    private void Line(Transform transform, List<Cell> availableCells, string param, int start, int end)
    {
        if (start == end) return;
        Vector3 possiblePosition = Vector3.zero;
        if (param == "x")
            possiblePosition = new Vector3(transform.position.x + start * 2, transform.position.y);
        else if (param == "y")
            possiblePosition = new Vector3(transform.position.x, transform.position.y + start * 2);
        var cell = Cells.GetCell(possiblePosition);
        if (cell != null)
        {
            if (cell.GetComponent<Cell>().IsFree())
                availableCells.Add(cell.GetComponent<Cell>());
            else if (cell.GetComponent<Cell>().Figure.Color != Color)
            {
                availableCells.Add(cell.GetComponent<Cell>());
                return;
            }
            else return;
        }
        if (start > end)
            Line(transform, availableCells, param, --start, end);
        if (start < end)
            Line(transform, availableCells, param, ++start, end);

    }

    private void Diagonal(Transform transform, List<Cell> availableCells, int startX, int endX, int startY, int endY)
    {
        if (startX == endX || startY == endY) return;
        var cell = Cells.GetCell(new Vector3(transform.position.x + startX * 2, transform.position.y + startY * 2));
        if (cell != null)
        {
            Cell currentCell = cell.GetComponent<Cell>();
            if (currentCell.IsFree())
                _availableCells.Add(currentCell);
            else if (currentCell.Figure.Color != Color)
            {
                _availableCells.Add(currentCell);
                return;
            }
        }
        if (startX < endX)
            if (startY < endY)
                Diagonal(transform, availableCells, ++startX, endX, ++startY, endY);
            else
                Diagonal(transform, availableCells, ++startX, endX, --startY, endY);
        else if (startY > endY)
            Diagonal(transform, availableCells, --startX, endX, --startY, endY);
        else
            Diagonal(transform, availableCells, --startX, endX, ++startY, endY);
    }
}
