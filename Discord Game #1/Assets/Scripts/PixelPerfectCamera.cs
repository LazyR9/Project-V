using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{
    public Camera mainCamera;
    private UnityEngine.U2D.PixelPerfectCamera pixelPerfectCamera;
    public GameObject background;

    // Update is called once per frame
    void Update()
    {
        pixelPerfectCamera = mainCamera.GetComponent<UnityEngine.U2D.PixelPerfectCamera>();
        pixelPerfectCamera.refResolutionX = Screen.currentResolution.width;
        pixelPerfectCamera.refResolutionY = Screen.currentResolution.height;
        if (background)
        {
            Vector3 k = background.transform.localScale;
            k.x = 2 * Camera.main.orthographicSize * Camera.main.aspect / background.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            k.y = 2 * Camera.main.orthographicSize / background.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            background.transform.localScale = k;
        }
    }
}
