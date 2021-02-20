using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    }

    public void SetLight()
    {
        foreach(var cell in _availableCells)
            cell.SetLight(UnityEngine.Color.green);
    }

    public void TryPreventCheck()
    {
        Transform previosCell = transform.parent;
        List<Cell> garbage = new List<Cell>();
        int n = _availableCells.Count;
        for (int i = 0; i < n; i++)
        {
            transform.SetParent(_availableCells[i].transform);
            Game.Check();
            FindGarbageCell(previosCell, garbage, i);
        }
        foreach (var cell in garbage)
            _availableCells.Remove(cell);

        if (_availableCells.Count != 0)
        {
            transform.SetParent(previosCell);
            Game.Check();
            SetLight();
        }
    }

    private void FindGarbageCell(Transform previosCell, List<Cell> garbage, int i)
    {
        if (Game.IsCheck(Color))
        {
            transform.SetParent(previosCell);
            garbage.Add(_availableCells[i]);
        }
        else
            _availableCells[i].SetLight(UnityEngine.Color.green);
    }

    public bool BrokeUp()
    {
        LightOff();
        if (_availableCells.Count == 0)
        {
            transform.position = _previousPosition;
            return false;
        }
        foreach (var cell in _availableCells)
        {
            if (cell.Position == Round(transform.position))
            {
                if (!cell.IsFree())
                {
                    Destroy(cell.Figure.gameObject);
                    Thread.Sleep(30);
                }
                transform.SetParent(cell.transform, false);
                transform.localPosition = new Vector3(0, 0, 0);
                _availableCells.Clear();
                if (Game.CurrentMove == Color)
                {
                    if (Color == FigureColor.White)
                        Game.CurrentMove = FigureColor.Black;
                    else
                        Game.CurrentMove = FigureColor.White;
                }
                StartCoroutine(Game.CheckAfterKill());
                break;
            }

        }
        if (_availableCells.Count > 0)
        {
            transform.position = _previousPosition;
            return false;
        }
        return true;
    }

    private void LightOff()
    {
        foreach (var cell in _availableCells)
            cell.LightOff();
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
