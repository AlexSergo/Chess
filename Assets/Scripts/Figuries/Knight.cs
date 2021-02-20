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
        SearchWay.KnightCells(transform, _availableCells);
        TryPreventCheck();
    }
}
