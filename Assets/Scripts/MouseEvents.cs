using UnityEngine;
using UnityEngine.UIElements;

public class MouseEvents : MonoBehaviour
{
    private const float RaycastRadius = 0.1f; 
    
    private Camera _camera;
    private Furniture _previousFurniture;
    private Furniture _currentSelectedFurniture;

    private void Awake()
    {
        _camera = Camera.main;

        if (_camera == null)
        {
            Debug.LogError("No camera found in scene.");
        }
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, RaycastRadius, out var hit))
        {
            var currentFurniture = hit.transform.GetComponentInParent<Furniture>();

            if (currentFurniture != null) // Current hover is a furniture.
            {
                if (currentFurniture != _previousFurniture) // Current hover is a different furniture.
                {
                    currentFurniture.Hover(true);

                    _previousFurniture?.Hover(false);
                    _previousFurniture = currentFurniture;  
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    _currentSelectedFurniture?.Click(false);
                    currentFurniture.Click(true);
                    _currentSelectedFurniture = currentFurniture;
                }
            }
            else
            {
                _previousFurniture?.Hover(false);
                _previousFurniture = null;

                if (Input.GetMouseButtonDown(0))
                {
                    _currentSelectedFurniture?.Click(false);
                    _currentSelectedFurniture = null;
                }
            }
        }
        else
        {
            _previousFurniture?.Hover(false);
            _previousFurniture = null;
            
            if (Input.GetMouseButtonDown(0))
            {
                _currentSelectedFurniture?.Click(false);
                _currentSelectedFurniture = null;
            }
        }
    }
}
