using System;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    private void Hover(bool enter)
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = enter ? Color.red : Color.white;
        }
    }
    
    private void OnMouseEnter()
    {
        Hover(true);
    }


    private void OnMouseExit()
    {
        Hover(false);
    }

    private void OnMouseDown()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.yellow;
        }  
    }
}
