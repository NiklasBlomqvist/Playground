using Playground;
using UnityEngine;

public class GroundHighlight : MonoBehaviour, ISelectable
{
    [SerializeField] private GameObject hoverObject;
    [SerializeField] private GameObject selectedObject;

    private void Awake()
    {
        hoverObject.SetActive(false);
        selectedObject.SetActive(false);
    }

    public void Hover(bool hover)
    {
        selectedObject.SetActive(false);
        hoverObject.SetActive(hover);
    }

    public void Click(bool click)
    {
        hoverObject.SetActive(false);
        selectedObject.SetActive(click);
    }
}
