using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float RaycastRadius = 0.1f;

    private Furniture _previousFurniture;
    private Furniture _currentSelectedFurniture;

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
            HandleHover(hit.transform.GetComponentInParent<Furniture>());

            if (Input.GetMouseButtonDown(0))
            {
                HandleSelection(hit.transform.GetComponentInParent<Furniture>());
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
    /// <param name="currentFurniture"></param>
    private void HandleHover(Furniture currentFurniture)
    {
        if (currentFurniture != null)
        {
            if (currentFurniture != _previousFurniture)
            {
                currentFurniture.Hover(true);
                _previousFurniture?.Hover(false);
                _previousFurniture = currentFurniture;
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
    /// <param name="currentFurniture"></param>
    private void HandleSelection(Furniture currentFurniture)
    {
        _currentSelectedFurniture?.Click(false);

        if (currentFurniture != null)
        {
            currentFurniture.Click(true);
            _currentSelectedFurniture = currentFurniture;
        }
        else
        {
            _currentSelectedFurniture = null;
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
        _currentSelectedFurniture?.Click(false);
        _currentSelectedFurniture = null;
    }
}