using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour // Don't judge me, I know this code sucks, "this is just a prototype" ;D
{
    // REFACTOR NOTES! Reenable detection of mouse input for screen panning
    // ALSO make this script better if there's any problem with it
    // also switch fully to the new input system


    private PlayerInput playerInput;
    private Camera cameraComponent;

    private float tempOrthoSize = 1f;
    private float multiplier = 1f;
    private float deltaTime = 1f;
    private float scrollWheelInput = 0f;
    private Vector3 tempPosition;

    [SerializeField]
    private float panBorderThickness = 10f;
    [SerializeField]
    private float panSpeed = 10f;
    [SerializeField]
    private float scrollSpeed = 100f;
    [SerializeField]
    private float baseMultiplier = 1f;
    [SerializeField]
    private float boostedMultiplier = 25f;
    [SerializeField]
    private Vector2 panLimit = new Vector2(120000,120000); // screen pan limits

    [SerializeField]
    private float orthoSizeUpperLimit = 120000f;
    [SerializeField]
    private float orthoSizeLowerLimit = 1f;
    [SerializeField]
    private float linearZoomUpperLimit = 49f;
    [SerializeField]
    private float linearZoomLowerLimit = 1f;
    [SerializeField]
    private float linearZoom;
    [SerializeField]
    private Vector2 zoomDeadZoneInScreenSizePercents = new Vector2(0.5f, 0.5f); // to avoid screen jitter, counts from the edges of the screen

    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        linearZoom = cameraComponent.orthographicSize;
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError(this + ": Cannot locate playerInput!");
        }
    }

    void Update()
    {
        if (playerInput.currentActionMap.name == "Gameplay") // work only during gameplay
        {
            deltaTime = Time.unscaledDeltaTime; // IMPORTANT! To make sure it's working when the game is paused

            tempPosition = transform.position;

            if (Input.GetKey("w")) // || Input.mousePosition.y >= Screen.height - panBorderThickness
            {
                tempPosition.y += panSpeed * deltaTime * multiplier;
            }

            if (Input.GetKey("s")) //  || Input.mousePosition.y <= panBorderThickness
            {
                tempPosition.y -= panSpeed * deltaTime * multiplier;
            }

            if (Input.GetKey("d")) //  || Input.mousePosition.x >= Screen.width - panBorderThickness
            {
                tempPosition.x += panSpeed * deltaTime * multiplier;
            }

            if (Input.GetKey("a")) //  || Input.mousePosition.x <= panBorderThickness
            {
                tempPosition.x -= panSpeed * deltaTime * multiplier;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                multiplier = boostedMultiplier;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                multiplier = baseMultiplier;
            }

            scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

            // implement curved value zoom

            linearZoom -= scrollWheelInput * scrollSpeed * deltaTime * multiplier;

            // limiting screen pan by clamping the max malues

            tempPosition.x = Mathf.Clamp(tempPosition.x, -panLimit.x, panLimit.x);
            tempPosition.y = Mathf.Clamp(tempPosition.y, -panLimit.y, panLimit.y);
            linearZoom = Mathf.Clamp(linearZoom, linearZoomLowerLimit, linearZoomUpperLimit);

            transform.position = tempPosition;
            ShiftCameraOnZoom();
            cameraComponent.orthographicSize = GetActualOrthoSize(linearZoom);
        }
    }

    /// <summary>
    /// Uses non-linear function to calculate camera orthographic size based on linear zoom level.
    /// </summary>
    /// <param name="linearZoomLevel"></param>
    /// <returns></returns>
    private float GetActualOrthoSize(float linearZoomLevel)
    {
        tempOrthoSize = linearZoomLevel * linearZoomLevel * linearZoomLevel;
        tempOrthoSize = Mathf.Clamp(tempOrthoSize, orthoSizeLowerLimit, orthoSizeUpperLimit);

        return tempOrthoSize;
    }

    private void ShiftCameraOnZoom()
    {
        if (scrollWheelInput == 0) return;

        // check if mouse is close to the center of the screen and if it is then RETURN
        // dead zone near the center of the screen

        if (CheckForZoomDeadZone()) return;

        transform.position = cameraComponent.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// See if the mouse is close to the center of the screen, within the dead space area.
    /// </summary>
    /// <returns>Returns bool TRUE if near the center of the screen, within the dead zone. Otherwise returns bool FALSE.</returns>
    private bool CheckForZoomDeadZone()
    {

        if (Input.mousePosition.x < ((Screen.width / 2) * zoomDeadZoneInScreenSizePercents.x)) return false;

        if (Input.mousePosition.x > (Screen.width - ((Screen.width/ 2) * zoomDeadZoneInScreenSizePercents.x))) return false;

        if (Input.mousePosition.y < ((Screen.height / 2) * zoomDeadZoneInScreenSizePercents.y)) return false;

        if (Input.mousePosition.y > (Screen.height - ((Screen.height / 2) * zoomDeadZoneInScreenSizePercents.y))) return false;

        // we're within dead zone now!

        return true;
    }

    // refactoring notes
    // maybe there should be a common (or a set of) modifiers that change with base zoom level (non-linearly) and affect all aspects
    // of camera controls -- panning speed, zooming speed, etc
}
