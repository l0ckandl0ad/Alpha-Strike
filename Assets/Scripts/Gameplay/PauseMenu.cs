using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private PlayerInput playerInput;

    private bool originalIsPausedState = true;
    string gameplayActionMapName = "Gameplay";
    string pauseMenuActionMapName = "PauseMenu";

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }
    public void OpenPauseMenu(PlayerInput playerInput)
    {
        originalIsPausedState = DateTimeModel.IsPaused;
        DateTimeModel.Pause();
        pauseMenu.SetActive(true);
        this.playerInput = playerInput;
        playerInput.SwitchCurrentActionMap(pauseMenuActionMapName);
    }
    /// <summary>
    /// Set resumePaused to True if you want to resume in Paused state, false for original state.
    /// </summary>
    public void Resume(bool resumePaused)
    {
        pauseMenu.SetActive(false);
        playerInput.SwitchCurrentActionMap(gameplayActionMapName);
        if (resumePaused)
        {
            // keep the game paused - it should still be paused after OpenPauseMenu ran
        }
        else
        {
            if (originalIsPausedState == false)
            {
                DateTimeModel.TogglePause();
            }
        }
    }
    public void Save()
    {
        GameManager.Instance.SaveGame();
        Resume(true);
    }

    public void Options()
    {
        // nothing here yet
    }
    public void Resign()
    {
        playerInput.SwitchCurrentActionMap(gameplayActionMapName); // probably unnecessary...
        GameManager.Instance.LoadMainMenu();
    }
    public void QuitToDesktop()
    {
        GameManager.Instance.QuitToDesktop();
    }
}
