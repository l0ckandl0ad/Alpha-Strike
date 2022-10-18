using System.Collections.Generic;

[System.Serializable]
public class SaveData : ISaveData
{
    private ISimulationData simulationData;
    private List<IFleetData> fleetDataList;

    public SaveData(ISimulationData simulationData, List<IFleetData> fleetDataList)
    {
        this.simulationData = simulationData;
        this.fleetDataList = fleetDataList;
    }
    public ISimulationData GetSimulationData()
    {
        return simulationData;
    }
    public List<IFleetData> GetFleetData()
    {
        return fleetDataList;
    }
}
