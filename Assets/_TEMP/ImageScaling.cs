using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scales the image in relation to camera's orthograthic size.
/// This is a monobehaviour that should be attached to a gameObject with image.
/// </summary>
public class ImageScaling : MonoBehaviour
{
    private float minScale = 1f;
    private float maxScale = 20f;
    private float orthographicSizeDivider = 800f;

    private Image image;
    private Camera cameraMain;
    private void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError(this + ": Image not found!");
        }

        cameraMain = Camera.main;
    }

    void Update()
    {
        float ratio = Mathf.Clamp((cameraMain.orthographicSize / orthographicSizeDivider), minScale, maxScale);

        Debug.Log(ratio.ToString());

        transform.localScale = Vector3.one * ratio;
    }
}
