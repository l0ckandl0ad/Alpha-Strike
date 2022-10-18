using System.Collections.Generic;

[System.Serializable]
public class FleetData : IFleetData
{
    public string Name { get; private set; }

    public IFF IFF { get; private set; }

    public List<IVessel> ShipList { get; private set; }

    public float PositionX { get; private set; }

    public float PositionY { get; private set; }

    public FleetData(string name, IFF iff, List<IVessel> shipList, float positionX, float positionY)
    {
        Name = name;
        IFF = iff;
        ShipList = shipList;
        PositionX = positionX;
        PositionY = positionY;
    }
}
