using System;
using System.Collections.Generic;

public interface ISaveData
{
    ISimulationData GetSimulationData();
    List<IFleetData> GetFleetData();
}
