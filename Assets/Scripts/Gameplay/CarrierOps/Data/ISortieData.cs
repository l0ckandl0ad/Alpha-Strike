using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortieData
{
    /// <summary>
    /// Gives craft commands if it is assigned to a fleet.
    /// </summary>
    void GetCommands(ICarrierCapable craft);
}
