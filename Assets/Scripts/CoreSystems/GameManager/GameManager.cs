using UnityEngine;
using UnityEngine.SceneManagement;
using AlphaStrike.Gameplay;

public class GameManager : Singleton<GameManager>
{
    // scene names --- temp/refactor later?
    private string mainMenu = "MainMenu";
    private Persistence persistence;
    
    public Simulation CurrentSimulation { get; private set; } // should it be static?

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        persistence = new Persistence();
    }

    public void SetCurrentSimulation(Simulation simulation)
    {
        CurrentSimulation = simulation;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Debug.Log(this + ": "+ mainMenu + " scene loaded");
    }
    
    public void StartNewGame()
    {
        persistence.StartNewGame();
    }

    public void LoadGame()
    {
        persistence.LoadGameFromSlot1();
    }

    public void SaveGame()
    {
        persistence.SaveGameToSlot1();
    }
    public void Quicksave()
    {
        persistence.Quicksave();
    }
    public void Quickload()
    {
        persistence.Quickload();
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
