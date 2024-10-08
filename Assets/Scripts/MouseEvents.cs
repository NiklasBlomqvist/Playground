using Playground;
using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float RaycastRadius = 0.1f;

    private ISelectable _previousFurniture;
    private ISelectable _currentSelectedSelectable;
    private bool _drag;
    private Vector3 _previousMousePosition;

    private void Update()
    {
        HandleMouseEvents();

        if (_drag)
        {
            _currentSelectedSelectable?.Drag(GetMouseWorldPosition());
        }
    }
    
    private Vector3 GetMouseWorldPosition()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var xyPlane = new Plane(Vector3.up, Vector3.zero);

        if (xyPlane.Raycast(ray, out float enter))
        {
            var hitPoint = ray.GetPoint(enter);
            return new Vector3(hitPoint.x, 0, hitPoint.z); // Ignore y-axis
        }

        return Vector3.zero;
    }

    /// <summary>
    /// Handle the hover and selection of furniture.
    /// </summary>
    private void HandleMouseEvents()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, RaycastRadius, out var hit))
        {
            HandleHover(hit.transform.GetComponentInParent<ISelectable>());

            if (Input.GetMouseButtonDown(0))
            {
                HandleSelection(hit.transform.GetComponentInParent<ISelectable>());
                _drag = true;
                
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                Debug.Log("Mouse World Position: " + mouseWorldPosition);
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

        if (Input.GetMouseButtonUp(0))
            _drag = false;
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