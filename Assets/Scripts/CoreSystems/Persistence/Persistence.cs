using UnityEngine;

public class Persistence
{
    private SaveFileHandler saveFileHandler;
    private ScenarioLoader scenarioLoader;
    private SaveDataGenerator saveDataGenerator;

    private string saveSlot1Path = "/SaveSlot1.save";
    private string quickSavePath = "/Quicksave.save";

    public Persistence()
    {
        saveFileHandler = new SaveFileHandler();
        scenarioLoader = new ScenarioLoader();
        saveDataGenerator = new SaveDataGenerator();
    }

    public void StartNewGame()
    {
        LoadGameFromSaveData(new NewGameSaveData());
    }
    public void LoadGameFromSlot1()
    {
        LoadGameFromSaveData(saveFileHandler.LoadDataFromFile(saveSlot1Path));
    }
    public void Quickload()
    {
        LoadGameFromSaveData(saveFileHandler.LoadDataFromFile(quickSavePath));
    }
    public void SaveGameToSlot1()
    {
        saveFileHandler.SaveDataToFile(saveDataGenerator.GenerateSaveData(), saveSlot1Path);
    }
    public void Quicksave()
    {
        saveFileHandler.SaveDataToFile(saveDataGenerator.GenerateSaveData(), quickSavePath);
    }

    private void LoadGameFromSaveData(ISaveData saveData)
    {
        if (GameManager.Instance != null && saveData != null )
        {
            GameManager.Instance.StartCoroutine(scenarioLoader.LoadAsyncScenario(saveData));
        }
        else
        {
            Debug.LogError(this + ".LoadGameFromSaveData() error: Either saveData or GameManager.Instance is null.");
        }
    }
}
