using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler
{
    public WindowBase windowBase;

    void Awake()
    {
        windowBase = transform.parent.GetComponent<WindowBase>();
    }

    public void OnDrag(PointerEventData data)
    {
        windowBase.OnDrag(data);
    }
}
