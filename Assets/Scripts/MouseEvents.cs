using Playground;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float raycastRadius = 0.1f;

    private ISelectable _currentHover;
    private ISelectable _currentSelection;

    private void Update()
    {
        HandleMouseEvents();
    }

    private void HandleMouseEvents()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray, raycastRadius, out var hit))
        {
            var selectable = hit.transform.GetComponentInParent<ISelectable>();
            HandleHover(selectable);

            if (Input.GetMouseButtonDown(0))
            {
                HandleSelection(selectable);
            }
        }
        else
        {
            ClearCurrentHover();

            if (Input.GetMouseButtonDown(0))
            {
                ClearCurrentSelection();
            }
        }
    }

    private void HandleHover(ISelectable newHover)
    {
        if (newHover == null)
        {
            ClearCurrentHover();
        }
        else if (newHover != _currentHover)
        {
            ClearCurrentHover();
            _currentHover = newHover;
            _currentHover.Hover(true);
        }
    }

    private void HandleSelection(ISelectable newSelectable)
    {
        ClearCurrentSelection();

        if (newSelectable != null)
        {
            _currentSelection = newSelectable;
            _currentSelection.Click(true);
        }
    }

    private void ClearCurrentHover()
    {
        _currentHover?.Hover(false);
        _currentHover = null;
    }

    private void ClearCurrentSelection()
    {
        _currentSelection?.Click(false);
        _currentSelection = null;
    }
}