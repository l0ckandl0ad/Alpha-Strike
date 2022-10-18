using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFlightControl : ICarrierFacility
{
    /// <summary>
    /// Max number of craft allowed on this carrier.
    /// </summary>
    int MaxNumberOfCraftAllowedTotal { get; }
    List<ICarrierCapable> CraftToReady { get; }
    List<ICarrierCapable> CraftToStandDown { get; }
    List<ICarrierCapable> CraftToLand { get; }
    List<ICarrierCapable> CraftToLaunch { get; }
    ICarrierCraftHoldingFacility Hangar { get; }
    ICarrierCraftHoldingFacility TransferArea { get; }
    IFlightDeck FlightDeck { get; }
    bool AddCraftToReady(ICarrierCapable craft);
    bool AddCraftToStandDown(ICarrierCapable craft);
    bool AddCraftToLand(ICarrierCapable craft);
    bool AddCraftToLaunch(ICarrierCapable craft);

    /// <summary>
    /// The main method for executing Flight Control Ops Logic. Runs from outisde (eg CarrierOps.cs) via Update.
    /// </summary>
    void RunOperations(ICarrierOpsHandler currentCarrierCommandHandler);
    ICarrierOpsHandler CurrentCarrierCommandHandler { get; }
}
