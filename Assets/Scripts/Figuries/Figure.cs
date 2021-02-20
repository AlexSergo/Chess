using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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

    public void SetLight()
    {
        foreach(var cell in _availableCells)
            cell.SetLight(UnityEngine.Color.green);
    }

    public void TryPreventCheck()
    {
        Transform previosCell = transform.parent;
        Transform availableKill = null;
        Transform availableKillCell = null;
        List<Cell> garbage = new List<Cell>();
        for (int i = 0; i < _availableCells.Count; i++)
        {
            if (!_availableCells[i].IsFree())
            {
                availableKill = _availableCells[i].Figure.transform;
                availableKillCell = _availableCells[i].transform;
                availableKill.SetParent(Camera.main.transform);
                availableKill.position = new Vector3(-100, -100);
            }
            transform.SetParent(_availableCells[i].transform);
            transform.localPosition = new Vector3(0,0,0);
            if (transform.GetComponent<King>() != null)
                Debug.Log("клетка короля: " + transform.position);
            Game.Check();
            if (transform.GetComponent<King>() != null)
            {
                if (Game.IsCheck(Color))
                    Debug.Log("Недоступная клетка - " + _availableCells[i].Position);
            }
            FindGarbageCell(previosCell, garbage, i);
            if (availableKill != null && availableKill.parent != availableKillCell)
            {
                availableKill.SetParent(availableKillCell);
                availableKill.localPosition = new Vector3(0, 0, 0);
            }
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
            if (transform.GetComponent<King>() != null)
                Debug.Log("Мусор - " + _availableCells[i].Position);
            transform.SetParent(previosCell);
            garbage.Add(_availableCells[i]);
        }
        else
        {
            _availableCells[i].SetLight(UnityEngine.Color.green);
        }
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
                    Destroy(cell.Figure.gameObject);

                transform.SetParent(cell.transform, false);
                transform.localPosition = new Vector3(0, 0, 0);
                _availableCells.Clear();
                if (Game.CurrentMove == Color)
                    if (Color == FigureColor.White)
                        Game.CurrentMove = FigureColor.Black;
                    else
                        Game.CurrentMove = FigureColor.White;

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
