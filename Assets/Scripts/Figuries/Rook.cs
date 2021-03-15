public class Rook : Figure
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;
        if (!IsMyCurrentMove()) return;
        SearchWay.RookCells(transform, _availableCells);
        TryPreventCheck();
    }

}
