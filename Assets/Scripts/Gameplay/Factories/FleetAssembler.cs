using AlphaStrike.Gameplay.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaStrike.Gameplay.Factories
{
    public static class FleetAssembler
    {
        private static GameObject fleetPrefabReference = Resources.Load<GameObject>("Prefabs/Fleet Prefab");

        public static void AssembleFleets(List<IFleetData> fleetDataList)
        {
            if (fleetDataList.Count > 0 && fleetPrefabReference != null)
            {
                Debug.Log("FleetAssembler: Assembling fleets...");
                foreach (IFleetData fleetData in fleetDataList)
                {
                    // setup starting fleet position
                    Vector3 fleetVector3Position = new Vector3(fleetData.PositionX, fleetData.PositionY);

                    // instantiate the prefab and get its Fleet component
                    GameObject prefabInstance = GameObject.Instantiate(fleetPrefabReference, fleetVector3Position,
                        Quaternion.identity);
                    Fleet fleet = prefabInstance.GetComponent<Fleet>();

                    // setup the rest of fleet data
                    fleet.SetName(fleetData.Name);
                    fleet.SetIFF(fleetData.IFF);
                    fleet.AddEntities(EntityUtils.VesselToEntityList(fleetData.ShipList));

                    // Report
                    Debug.LogFormat("FleetAssembler: fleet {0} assembled with {1} vessels ({2}).",
                        fleetData.Name, fleetData.ShipList.Count, fleetData.IFF.ToString());
                }
                Debug.Log("FleetAssembler: FINISHED assembling fleets.");
            }
            else
            {
                Debug.LogError("FleetAssembler: Something went wrong with assembling fleets.");
            }
        }
    }
}
