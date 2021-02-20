using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public Figure Figure 
    { 
        get 
        { 
            if (transform.childCount != 0)
                 return transform.GetChild(0).gameObject.GetComponent<Figure>();
            return null;
        } 
        private set { } 
    }
    public void SetLight(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private void Start()
    {
        Position = transform.position;
    }

    public bool IsFree()
    {
        if (transform.childCount < 1)
        {
            return true;
        }
        return false;
    }

    public void LightOff()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }
}
