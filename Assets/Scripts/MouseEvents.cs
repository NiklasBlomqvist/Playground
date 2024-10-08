using Playground;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float RaycastRadius = 0.1f;

    private ISelectable _previousFurniture;
    private ISelectable _currentSelectedSelectable;

    private void Update()
    {
        HandleHoverAndSelection();
    }

    /// <summary>
    /// Handle the hover and selection of furniture.
    /// </summary>
    private void HandleHoverAndSelection()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, RaycastRadius, out var hit))
        {
            HandleHover(hit.transform.GetComponentInParent<ISelectable>());

            if (Input.GetMouseButtonDown(0))
            {
                HandleSelection(hit.transform.GetComponentInParent<ISelectable>());
            }
        }
        else
        {
            ClearHover();

            if (Input.GetMouseButtonDown(0))
            {
                ClearSelection();
            }
        }
    }

    /// <summary>
    /// Handle the hover of furniture.
    /// </summary>
    /// <param name="currentSelectable"></param>
    private void HandleHover(ISelectable currentSelectable)
    {
        if (currentSelectable != null)
        {
            if (currentSelectable != _previousFurniture)
            {
                currentSelectable.Hover(true);
                _previousFurniture?.Hover(false);
                _previousFurniture = currentSelectable;
            }
        }
        else
        {
            ClearHover();
        }
    }

    /// <summary>
    /// Handle the selection of furniture.
    /// </summary>
    /// <param name="currentSelectable"></param>
    private void HandleSelection(ISelectable currentSelectable)
    {
        _currentSelectedSelectable?.Click(false);

        if (currentSelectable != null)
        {
            currentSelectable.Click(true);
            _currentSelectedSelectable = currentSelectable;
        }
        else
        {
            _currentSelectedSelectable = null;
        }
    }

    /// <summary>
    /// Clear the hover of furniture.
    /// </summary>
    private void ClearHover()
    {
        _previousFurniture?.Hover(false);
        _previousFurniture = null;
    }

    /// <summary>
    ///  Clear the selection of furniture.
    /// </summary>
    private void ClearSelection()
    {
        _currentSelectedSelectable?.Click(false);
        _currentSelectedSelectable = null;
    }
}