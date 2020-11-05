using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    public Vector2 minSize;
    public Vector2 maxSize;

    private RectTransform resizeArea;
    private RectTransform moveArea;
    private RectTransform toResize;
    private Vector2 currentPointerPosition;
    private Vector2 previousPointerPosition;

    private bool x = false;
    private bool y = false;
    private bool reverseX = false;
    private bool reverseY = false;

    private Vector2 pivot;

    private Vector2 screenSize;

    void Awake()
    {
        resizeArea = transform.Find("Resize Area").GetComponent<RectTransform>();
        moveArea = transform.Find("Move Area").GetComponent<RectTransform>();
        toResize = GetComponent<RectTransform>();

        screenSize = new Vector2(Screen.width, Screen.height);
        maxSize = new Vector2(Screen.width - 100, Screen.height - 100);
    }

    public void OnPointerDown(PointerEventData data)
    {
        resizeArea.SetAsLastSibling();

        print(RectTransformUtility.ScreenPointToLocalPointInRectangle(moveArea, data.position, data.pressEventCamera, out previousPointerPosition));
        print("You clicked on:");
        print(previousPointerPosition);

        print(RectTransformUtility.ScreenPointToLocalPointInRectangle(resizeArea, data.position, data.pressEventCamera, out previousPointerPosition));
        print("You clicked on:");
        print(previousPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        if (resizeArea == null)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(resizeArea, data.position, data.pressEventCamera, out currentPointerPosition);

        if (previousPointerPosition.x < -(toResize.sizeDelta.x / 2) + 5)
        {
            x = true;
            reverseX = true;
            pivot = new Vector2(1, toResize.pivot.y);
        }
        if (previousPointerPosition.y < -(toResize.sizeDelta.y / 2) + 5)
        {
            y = true;
            reverseY = true;
            pivot = new Vector2(toResize.pivot.x, 1);
        }
        if (previousPointerPosition.x > (toResize.sizeDelta.x / 2) - 5)
        {
            x = true;
            pivot = new Vector2(0, toResize.pivot.y);
        }
        if (previousPointerPosition.y > (toResize.sizeDelta.y / 2) - 5)
        {
            y = true;
            pivot = new Vector2(toResize.pivot.x, 0);
        }

        //toResize.anchorMax = toResize.pivot;
        //toResize.anchorMin = toResize.pivot;

        Vector2 size = toResize.rect.size;
        Vector2 deltaPivot = toResize.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);

        toResize.pivot = pivot;
        toResize.localPosition -= deltaPosition;

        Resize(x, y, reverseX, reverseY);

        x = false;
        y = false;
        reverseX = false;
        reverseY = false;

        previousPointerPosition = currentPointerPosition;
    }

    void Update()
    {
        if (screenSize != new Vector2(Screen.width, Screen.height))
        {
            maxSize = new Vector2(Screen.width - 100, Screen.height - 100);
            screenSize = new Vector2(Screen.width, Screen.height);
            Resize(true, true, false, false);
        }
    }

    void Resize(bool x, bool y, bool reverseX, bool reverseY)
    {
        Vector2 sizeDelta = toResize.sizeDelta;
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        if (!x)
        {
            resizeValue.x = 0;
        }
        if (!y)
        {
            resizeValue.y = 0;
        }
        if (reverseX)
        {
            resizeValue.x *= -1;
        }
        if (reverseY)
        {
            resizeValue.y *= -1;
        }

        sizeDelta += new Vector2(resizeValue.x, resizeValue.y);
        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
        );

        toResize.sizeDelta = sizeDelta;

        //toResize.pivot = new Vector2(0.5f, 0.5f);
        
        //toResize.anchorMax = Vector2.zero;
        //toResize.anchorMin = Vector2.zero;
    }
}