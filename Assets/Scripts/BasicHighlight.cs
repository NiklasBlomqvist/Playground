using Playground;
using UnityEngine;

public class BasicHighlight : MonoBehaviour, ISelectable
{
    private Renderer[] _renderers;
    private Material _defaultMaterial;
    private Material _hoverMaterial;
    private Material _selectedMaterial;

    private void Awake()
    {
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
        _defaultMaterial = _renderers[0].material;
        
        _hoverMaterial = Resources.Load<Material>("Hover");
        _selectedMaterial = Resources.Load<Material>("Selected");
    }

    public void Hover(bool hover)
    {
        foreach (var r in _renderers)
        {
            r.material = hover ? _hoverMaterial : _defaultMaterial;
        }
    }

    public void Select(bool select)
    {
        foreach (var r in _renderers)
        {
            r.material = select ? _selectedMaterial : _defaultMaterial;
        }
    }
}