using AlphaStrike.Gameplay.Entities;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataGenerator
{
    public SaveData GenerateSaveData()
    {
        List<IFleetData> fleetDataList = GenerateFleetData();

        if (GameManager.Instance.CurrentSimulation != null)
        {
            ISimulationData simulationData = GameManager.Instance.CurrentSimulation.SimulationData;

            // Kinda temp solution. Updating dateTime. This is important because it is not updated at runtime yet!
            simulationData.SetDateTime(DateTimeModel.GetCurrentDateTime());

            Debug.Log(this + ": Generated SaveData with number of fleets saved: " + fleetDataList.Count.ToString());
            return new SaveData(simulationData, fleetDataList);
        }
        else
        {
            Debug.LogError(this + ": GameManager.Instance.CurrentSimulation is null! No simulation to save from!");
            return null;
        }
    }

    private List<IFleetData> GenerateFleetData()
    {
        List<IFleetData> fleetDataList = new List<IFleetData>();

        Fleet[] fleets = Object.FindObjectsOfType<Fleet>();
        if (fleets.Length > 0)
        {
            foreach (Fleet fleet in fleets)
            {
                float x = fleet.GameObject.transform.position.x;
                float y = fleet.GameObject.transform.position.y;

                FleetData fleetData = new FleetData(fleet.Name, fleet.IFF, 
                    EntityUtils.EntityToVesselList(fleet.EntityList), x, y);
                fleetDataList.Add(fleetData);
            }
            return fleetDataList;
        }
        else
        {
            Debug.Log(this + ": No fleets found to save data from!");
            return null;
        }
    }
}
