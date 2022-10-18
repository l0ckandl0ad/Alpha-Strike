using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInput : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnOpenPauseMenu(InputValue value)
    {
        pauseMenu.OpenPauseMenu(playerInput);
    }
    private void OnClosePauseMenu(InputValue value)
    {
        pauseMenu.Resume(false);
    }
    private void OnQuicksave(InputValue value)
    {
        GameManager.Instance.Quicksave();
    }
    private void OnQuickload(InputValue value)
    {
        GameManager.Instance.Quickload();
    }
    private void OnTogglePause(InputValue value)
    {
        DateTimeModel.TogglePause();
    }
}
