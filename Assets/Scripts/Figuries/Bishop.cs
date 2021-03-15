using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        if (!IsMyCurrentMove()) return;
        SearchWay.BishopCells(transform, _availableCells);
        TryPreventCheck();
    }
}
