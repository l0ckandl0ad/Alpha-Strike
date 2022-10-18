using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AlphaStrike.Gameplay;
using AlphaStrike.Gameplay.Routines;
using AlphaStrike.Gameplay.Factories;

// Deals with setting up a scenario from ISaveData - loading scene, units etc.
public class ScenarioLoader
{
    /// <summary>
    /// This coroutine needs to be started via MonoBehaviour (eg GameManager.Instance)
    /// </summary>
    public IEnumerator LoadAsyncScenario(ISaveData saveData)
    {
        string sceneToLoad = saveData.GetSimulationData().GetSceneName();
        string scenarioName = saveData.GetSimulationData().GetScenarioName();

        Debug.Log(this + ": Starting to load " + scenarioName + " scenario. Loading " + sceneToLoad + " scene.");

        AsyncOperation asyncSceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncSceneLoading.isDone)
        {
            yield return null;
        }

        if (asyncSceneLoading.isDone)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
            SetupScenario(saveData);
        }

    }

    private void SetupScenario(ISaveData saveData)
    {
        string sceneToLoad = saveData.GetSimulationData().GetSceneName();
        string scenarioName = saveData.GetSimulationData().GetScenarioName();
        string activeSceneName = SceneManager.GetActiveScene().name;
        
        if (activeSceneName == sceneToLoad)
        {
            Debug.Log(this + ": Setting up simulation for " + scenarioName + " in scene " + activeSceneName);
            CreateSimulation(saveData.GetSimulationData());
            AssembleFleets(saveData);
            StartGameplayRoutines();
            // setup scenario further....
            Debug.Log(this + ": FINISHED setting up scenario " + scenarioName + " in " + sceneToLoad + " scene.");
        }
        else
        {
            Debug.LogError(this + ": Error when setting up scenario " + scenarioName +
                ". Scene expected: " + sceneToLoad + ", but we're in " + activeSceneName + " instead.");
        }
    }

    private void CreateSimulation(ISimulationData simulationData)
    {
        GameObject simulationGameObject = new GameObject("[Simulation]");
        Simulation simulation = simulationGameObject.AddComponent<Simulation>();
        simulation.Initialize(simulationData);
        GameManager.Instance.SetCurrentSimulation(simulation);
    }

    private static void AssembleFleets(ISaveData saveData)
    {
        // Try assembling the fleets from FleetData list
        List<IFleetData> fleetDataList = saveData.GetFleetData();
        if (fleetDataList != null && fleetDataList.Count > 0)
        {
            FleetAssembler.AssembleFleets(fleetDataList);
        }
    }

    private void StartGameplayRoutines()
    {
        GameObject routinesameObject = new GameObject("[GameplayRoutines]");
        GameplayRoutines gameplayRoutines = routinesameObject.AddComponent<GameplayRoutines>();
    }
}
