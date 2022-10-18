using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour // Don't judge me, I know this code sucks, "this is just a prototype" ;D
{
    // REFACTOR NOTES! Reenable detection of mouse input for screen panning
    // ALSO make this script better if there's any problem with it


    [SerializeField] private PlayerInput playerInput;
    private Camera cameraObject;

    public float panBorderThickness = 10f;

    public float panSpeed = 200f;
    public float scrollSpeed = 10000f;
    public float multiplier = 1f; // universal multiplier for all speeds

    public Vector2 panLimit = new Vector2(150000,266666); // screen pan limits

    public float zoomLimit = 80000;
    public float zoom;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GetComponent<Camera>();
        zoom = cameraObject.orthographicSize;
        if (playerInput == null)
        {
            Debug.LogError(this + ": Cannot locate playerInput!");
        }
    }

    void Update()
    {
        if (playerInput.currentActionMap.name == "Gameplay") // work only during gameplay
        {
            float deltaT = Time.unscaledDeltaTime; // IMPORTANT! To make sure it's working when the game is paused

            Vector3 pos = transform.position;

            if (Input.GetKey("w")) // || Input.mousePosition.y >= Screen.height - panBorderThickness
            {
                pos.y += panSpeed * deltaT * multiplier;
            }

            if (Input.GetKey("s")) //  || Input.mousePosition.y <= panBorderThickness
            {
                pos.y -= panSpeed * deltaT * multiplier;
            }

            if (Input.GetKey("d")) //  || Input.mousePosition.x >= Screen.width - panBorderThickness
            {
                pos.x += panSpeed * deltaT * multiplier;
            }

            if (Input.GetKey("a")) //  || Input.mousePosition.x <= panBorderThickness
            {
                pos.x -= panSpeed * deltaT * multiplier;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                multiplier = 20f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                multiplier = 1f;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");


            zoom -= scroll * scrollSpeed * deltaT * multiplier; //// NOPE, NOT THE CAMERA Z AXIS! WE'RE IN 2D, DUMMY!

            // limiting screen pan by clamping the max malues

            pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
            zoom = Mathf.Clamp(zoom, 100f, zoomLimit);

            transform.position = pos;
            cameraObject.orthographicSize = zoom;
        }
    }
}
