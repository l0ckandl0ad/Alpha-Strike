using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanReadyCraft : ICarrierFacility
{
    bool CanReadyCraft { get; }
    float ReadyingDelayInSeconds { get; }
    bool ChangeReadiness(ICarrierCapable craft, CarrierOpsState newReadinessState);
    bool ReadyCraft(ICarrierCapable craft);
    bool StandDownCraft(ICarrierCapable craft);
}
