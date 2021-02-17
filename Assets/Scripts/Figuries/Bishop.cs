using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Queen
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;

        FindDiagonalCells(transform, _availableCells);
    }
}
