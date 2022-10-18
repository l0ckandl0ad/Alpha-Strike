[System.Serializable]
public class SimulationData : ISimulationData
{
    private string scenarioName;
    private string sceneName;
    private int[] dateTime;
    private IntelData intelData;
    /// <summary>
    /// This non-monobehaviour object is a data container for scenario-related information.
    /// </summary>
    public SimulationData(string scenarioName, string sceneName, int[] dateTime)
    {
        this.scenarioName = scenarioName;
        this.sceneName = sceneName;
        this.dateTime = dateTime;
        intelData = new IntelData();
    }

    public string GetScenarioName()
    {
        return scenarioName;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public void SetDateTime(int[] dateTime)
    {
        this.dateTime = dateTime;
        // For now, we update dateTime here only on saving (see references to this method).
        // Refactor note - maybe it's better to run DateTime from Simulation by updating SimulationData?
    }

    public int[] GetDateTime()
    {
        return dateTime;
    }

    public IntelData GetIntelData()
    {
        return intelData;
    }
}