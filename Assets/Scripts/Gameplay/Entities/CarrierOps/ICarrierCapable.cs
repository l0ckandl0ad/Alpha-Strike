using System;
/// <summary>
/// Smaller craft that is capable of landing and launching from a carrier
/// </summary>
public interface ICarrierCapable : IVessel
{
    /// <summary>
    /// Is this craft eligible for routine search ops?
    /// </summary>
    bool IsAllowedForSearchOps { get; }
    /// <summary>
    /// Is this craft currently assigned to a sortie?
    /// </summary>
    bool IsSortied { get; }
    ISortieData SortieData { get; }
    CarrierOpsState CarrierOpsState { get; }
    /// <summary>
    /// To compare when trying can fit into hangar.
    /// </summary>
    int HangarSize { get; }
    /// <summary>
    /// Home for this craft.
    /// </summary>
    ICarrier Homeplate { get; }
    /// <summary>
    /// Set home for this craft.
    /// </summary>
    void SetHomeplate(ICarrier carrier);
    /// <summary>
    /// Current location of this craft on the carrier (if any).
    /// </summary>
    ICarrierCraftHoldingFacility CurrentHoldingFacility { get; }
    /// <summary>
    /// Bind craft to facility it should be in.
    /// </summary>
    void BindToFacility(ICarrierCraftHoldingFacility facility);
    /// <summary>
    /// Allow facilities to change time when this craft will be available for handling next.
    /// </summary>
    void SetAvailabilityDelay(float delayFromCurrentTimeInSeconds);
    /// <summary>
    /// Can facilities handle this craft now or is it busy, delayed by something? Is it even alive?
    /// </summary>
    bool IsAvailableForHandling();
    void SetCarrierOpsState(CarrierOpsState newState);
    /// <summary>
    /// A way to assign craft mission data and make it unavailable for other assignments.
    /// </summary>
    /// <param name="sortie"></param>
    void AssignSortie(ISortieData sortie);
    /// <summary>
    /// A way to clear info for a craft once sortie was cancelled or upon landing.
    /// </summary>
    void ClearSortieData();
}
