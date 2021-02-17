using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Figure : MonoBehaviour
{
    protected List<Cell> _availableCells = new List<Cell>();
    protected Vector3 _previousPosition;
    public FigureColor Color;

    private void Start()
    {
        if (transform.position.y >= 0 && transform.position.y <= 2)
            Color = FigureColor.Black;
        else
            Color = FigureColor.White;
    }

    private void OnMouseDrag()
    {
        Keep();
    }

    private void OnMouseDown()  
    {
        //_previousPosition = transform.position;
    }

    public void BrokeUp()
    {
        //var cell = Cells.GetCell(transform.position);
        //if (cell.gameObject.GetComponent<Cell>().IsFree())
        //     transform.position = _previousPosition;
        //else
        //{
        //    transform.SetParent(cell, false);
        //    transform.localPosition = new Vector3(0, 0, 0);
        //}
        if (_availableCells.Count == 0)
        {
            transform.position = _previousPosition;
            return;
        }
        foreach (var cell in _availableCells)
        {
            if (cell.Position == Round(transform.position))
            {
                if (!cell.IsFree())
                {
                    Destroy(cell.Figure.gameObject);
                }
                transform.SetParent(cell.transform, false);
                transform.localPosition = new Vector3(0, 0, 0);
                _availableCells.Clear();
                break;
            }

        }
        if (_availableCells.Count > 0)
            transform.position = _previousPosition;
    }

    private Vector3 Round(Vector3 position)
    {
        return new Vector3((int)Math.Round(position.x), (int)Math.Round(position.y));
    }

    public void Keep()
    {
        Vector3 cursor = Input.mousePosition;
        cursor = Camera.main.ScreenToWorldPoint(cursor);

        transform.position = new Vector3(cursor.x, cursor.y, 0);
    }
}
