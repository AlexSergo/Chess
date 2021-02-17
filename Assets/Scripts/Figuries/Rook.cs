public class Rook : Queen
{
    private void OnMouseUp()
    {
        BrokeUp();
    }

    private void OnMouseDown()
    {
        _previousPosition = transform.position;

        FindLineCells(transform, _availableCells);
    }

}
