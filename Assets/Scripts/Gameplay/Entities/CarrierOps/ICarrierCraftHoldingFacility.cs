using System.Collections.Generic;

public interface ICarrierCraftHoldingFacility : ICarrierFacility, ICanReadyCraft
{
    /// <summary>
    /// List of entities currently present at this facility.
    /// </summary>
    List<ICarrierCapable> CarrierCraftList { get; }
    int MaxNumberOfCraftAllowed { get; }
    /// <summary>
    /// Compared with ICarrierCapable's HangarSize to see if it can fit.
    /// </summary>
    int MaxCraftSizeAllowed { get; }
    /// <summary>
    /// Flight Control facility that's in charge of commanding this one.
    /// </summary>
    IFlightControl FlightControl { get; }
    /// <summary>
    /// How long it takes to transfer craft to another facility, in seconds.
    /// </summary>
    float TransferDelayInSeconds { get; }
    void BindToFlightControl(IFlightControl flightControl);
    /// <summary>
    /// Transfer specific craft to another facility and bind it to it, returns true if succesful.
    /// </summary>
    bool TransferCraftToFacility(ICarrierCapable craft, ICarrierCraftHoldingFacility newFacility);
    /// <summary>
    /// A way to teleport carrier capable craft to this facility bypassing any constraints.
    /// </summary>
    void TeleportCraftHere(ICarrierCapable craft);
    /// <summary>
    /// A way to teleport carrier capable craft to this facility bypassing any constraints.
    /// </summary>
    void TeleportCraftHere(List<ICarrierCapable> craftList);
    /// <summary>
    /// A way of removing craft from this facility bypassing any constraints.
    /// </summary>
    void RemoveCraftFromHere(ICarrierCapable craft);
    /// <summary>
    /// A way of removing craft from this facility bypassing any constraints.
    /// </summary>
    void RemoveCraftFromHere(List<ICarrierCapable> craftList);
}
