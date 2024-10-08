using Playground;
using UnityEngine;

public class BasicHighlight : MonoBehaviour, ISelectable
{
    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    public void Hover(bool hover)
    {
        foreach (var r in _renderers)
        {
            r.material.color = hover ? Color.red : Color.white;
        }
    }

    public void Click(bool click)
    {
        foreach (var r in _renderers)
        {
            r.material.color = click ? Color.yellow : Color.white;
        }
    }
}