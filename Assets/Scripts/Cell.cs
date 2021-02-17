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


}
