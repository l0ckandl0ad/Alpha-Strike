using System.Collections.Generic;

public interface IFleetData
{
    string Name { get; }
    IFF IFF { get; }
    List<IVessel> ShipList { get; }
    float PositionX { get; }
    float PositionY { get; }

}
