using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can ready, launch and land craft.
/// </summary>
public interface IFlightDeck : ICarrierCraftHoldingFacility
{
    bool LaunchCraft(ICarrierCapable craft);
    bool LandCraft(ICarrierCapable craft);
}
