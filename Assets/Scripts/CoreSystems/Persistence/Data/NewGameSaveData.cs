using System.Collections.Generic;

// Creates SaveData manually for a New Game start
public class NewGameSaveData : ISaveData
{
    private string scenarioName = "Grand Campaign";
    private string sceneName = "Gameplay";
    private int[] dateTime = { 2150, 9, 1, 0, 0, 0, 0 };
    private List<IFleetData> fleetDataList = new List<IFleetData>();
    private ISimulationData simulationData;

    public NewGameSaveData() 
    {
        simulationData = new SimulationData(scenarioName, sceneName, dateTime);

        // This is a test setup, will get more complicated later
        // Creating a list of ship names
        List<string> bluforDestroyerNamesList = new List<string>();
        bluforDestroyerNamesList.Add("Vigilant");
        bluforDestroyerNamesList.Add("Scrum");
        bluforDestroyerNamesList.Add("Guardian");
        bluforDestroyerNamesList.Add("Inescapable");


        List<string> opforShipNamesList = new List<string>();
        opforShipNamesList.Add("Desolator");
        opforShipNamesList.Add("Agressor");
        opforShipNamesList.Add("Provocator");

        List<string> opforShipNamesList2 = new List<string>();
        opforShipNamesList2.Add("Event Horizon");

        List<string> opforShipNamesList3 = new List<string>();
        opforShipNamesList3.Add("Hawk");
        opforShipNamesList3.Add("Harpy");


        // Creating ships from the list of names
        List<IVessel> bluforShipList = new List<IVessel>();
        List<IVessel> opforShipList = new List<IVessel>();
        List<IVessel> opforShipList2 = new List<IVessel>();

        bluforShipList = Shipyard.BuildShips("BluforDD", bluforDestroyerNamesList);
        bluforShipList.Add(Shipyard.BuildShip("BluforCV", "Langley"));

        opforShipList = Shipyard.BuildShips("OpforDD", opforShipNamesList);

        opforShipList2 = Shipyard.BuildShips("OpforCA", opforShipNamesList2);
        opforShipList2.AddRange(Shipyard.BuildShips("OpforDD", opforShipNamesList3));

        // Set up FleetData based on ships created
        IFleetData bluforFleet1 = new FleetData("BLUFOR FLEET 1", IFF.BLUFOR, bluforShipList, 0f, -300f);
        fleetDataList.Add(bluforFleet1);

        IFleetData opforFleet1 = new FleetData("OPFOR FLEET 1", IFF.OPFOR, opforShipList, 0f, 150f);
        fleetDataList.Add(opforFleet1);

        IFleetData opforFleet2 = new FleetData("OPFOR FLEET 2", IFF.OPFOR, opforShipList2, 0f, 190f);
        fleetDataList.Add(opforFleet2);
    }
    public List<IFleetData> GetFleetData()
    {
        return fleetDataList;
    }

    public ISimulationData GetSimulationData()
    {
        return simulationData;
    }
}