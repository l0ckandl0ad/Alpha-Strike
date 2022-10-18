using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // this script assumes that GameManager already exists (thanks to BOOT.cs)
    // keep that in mind next time you make changes to it

    public void StartNewGame()
    {
        GameManager.Instance.StartNewGame();
    }
    public void LoadGame()
    {
        GameManager.Instance.LoadGame();
    }
    public void QuitToDesktop()
    {
        GameManager.Instance.QuitToDesktop();
    }
}
