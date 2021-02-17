using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        FindAvailableCells("x", -1, -8);
        FindAvailableCells("x", 1, 8);
        FindAvailableCells("y", -1, -8);
        FindAvailableCells("y", 1, 8);
    }

    private void FindAvailableCells(string param, int start, int end)
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
                    _availableCells.Add(cell.GetComponent<Cell>());
                else if (cell.GetComponent<Cell>().Figure.Color != Color)
                {
                    _availableCells.Add(cell.GetComponent<Cell>());
                    return;
                }
                else return;
            }
        if (start > end)
            FindAvailableCells(param, --start, end);
        if (start < end)
            FindAvailableCells(param, ++start, end);

    }
}
