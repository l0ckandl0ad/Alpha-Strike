using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Binds MapEntity Panel's action buttons to their commands and implements them.
/// </summary>
/// <remarks>
/// It uses lotsa old Input system features and hard references to systems (eg MapEntitySelectionModel, MoveCommand).
/// So it needs to be refactored into something more solid when practical.
/// </remarks>
public class ActivePlatformGroupActions : MonoBehaviour
{
    [Header("ACTION BUTTONS")]
    [SerializeField] private Button setWaypointButton;
    [SerializeField] private Button cancelAllButton;

    private IFleetCommandHandler currentFleetCommandHandler;
    private CommandState SetWaypointState = CommandState.READY;

    private void OnEnable()
    {
        // bind buttons to actions
        cancelAllButton.onClick.AddListener(() => CancelAllCommands());
        setWaypointButton.onClick.AddListener(() => SetWaypoint());
    }

    private void OnDisable()
    {
        // unbind all event listners from the action buttons
        cancelAllButton.onClick.RemoveAllListeners();
        setWaypointButton.onClick.RemoveAllListeners();
    }

    private void UpdateCurrentCommandable()
    {
        if (MapEntitySelectionModel.SelectedGameObject != null
            && MapEntitySelectionModel.SelectedGameObject.TryGetComponent(out IFleetCommandHandler commandable))
        {
            currentFleetCommandHandler = commandable;
        } 
    }

    private void CancelAllCommands()
    {
        UpdateCurrentCommandable();
        if (currentFleetCommandHandler != null)
        {
            currentFleetCommandHandler.RemoveAllCommands();
        }
    }

    private void SetWaypoint()
    {
        UpdateCurrentCommandable();
        if (currentFleetCommandHandler != null)
        {
            if (SetWaypointState == CommandState.READY)
            {
                currentFleetCommandHandler.StartCoroutine(SetWaypointCoroutine());
                SetWaypointState = CommandState.RUNNING;
            }
        }
    }

    private IEnumerator SetWaypointCoroutine()
    {
        MapEntitySelectionModel.ToggleSelectionAllowed();
        while (true)
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift)) // single waypoint addded on LMB click
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    currentFleetCommandHandler.RemoveAllCommands();
                    Vector3 mouseCoordinates = new Vector3();
                    mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseCoordinates.z = 0f;
                    new MoveToVector(currentFleetCommandHandler, mouseCoordinates);
                    //Debug.Log("coroutine stops via single click check");
                    SetWaypointState = CommandState.READY; // ready to set the next waypoint
                    MapEntitySelectionModel.ToggleSelectionAllowed();
                    yield break; //stop the coroutine!
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 mouseCoordinates = new Vector3();
                    mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseCoordinates.z = 0f;
                    new MoveToVector(currentFleetCommandHandler, mouseCoordinates);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SetWaypointState = CommandState.READY; // ready to set the next waypoint
                MapEntitySelectionModel.ToggleSelectionAllowed();
                yield break; //stop the coroutine upon GetKeyUp(LeftShift)!
            }
            yield return null;
        }
    }
    private void OnDestroy()
    {
        MapEntitySelectionModel.Initialize(); // reset state if coroutine on this monobehaviour ended prematurely
    }
}
