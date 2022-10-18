using System.Collections.Generic;
using UnityEngine;

public static class Shipyard
{
    public static List<IVessel> BuildShips (string className, List<string> shipNameList)
    {
        if (shipNameList.Count > 0)
        {
            List<IVessel> shipList = new List<IVessel>();
            foreach (string shipName in shipNameList)
            {
                shipList.Add(BuildShip(className, shipName));
            }
            return shipList;
        }
        else
        {
            Debug.LogWarning("[Shipyard]: Shipyard.BuildShips recieved an empty shipNameList parameter.");
            return null;
        }

    }

    public static IVessel BuildShip(string className, string shipName)
    {
        switch (className)
        {
            case "BluforCV":
                return new BluforCV(shipName);
            case "BluforDD":
                return new BluforDD(shipName);
            case "OpforDD":
                return new OpforDD(shipName);
            case "OpforCA":
                return new OpforCA(shipName);
            case "something else":
                return null;
            default:
                Debug.LogError("[Shipyard]: Shipyard.BuildShip() input className not found: " + className);
                return null;
        }
    }


}
