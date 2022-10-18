using System.Collections.Generic;

public interface ICarrierOpsHandler : ICommandable
{
    List<IFlightControl> FlightControlList { get; }
    List<ICarrierFacility> AllCarrierFacilities { get;}
    IRoutineCarrierOpsSettings RoutineOpsSettings { get; }
    bool TryGetComponent<T>(out T component);
    /// <summary>
    /// Try running a routine search immediately.
    /// </summary>
    void RunRoutineSearchNow();
}
