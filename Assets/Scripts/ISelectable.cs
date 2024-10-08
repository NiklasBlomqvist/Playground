using UnityEngine;

namespace Playground
{
    public interface ISelectable
    {
        void Hover(bool hover);
        void Click(bool click);
        void Drag(Vector3 targetPosition);
    }
}