using System;
using UnityEngine;

[System.Serializable]
public abstract class CarrierCraft : CombatVessel, ICarrierCapable
{
    public bool IsAllowedForSearchOps { get; protected set; } = false;
    public bool IsSortied { get; protected set; } = false;
    public ISortieData SortieData { get; protected set; }
    public int HangarSize { get; protected set; } = 10;
    public ICarrier Homeplate { get; protected set; }
    public ICarrierCraftHoldingFacility CurrentHoldingFacility { get; protected set; }
    public CarrierOpsState CarrierOpsState { get; protected set; } = CarrierOpsState.Unready;
    protected DateTime AvailableForHandling;
    public void SetHomeplate(ICarrier carrier)
    {
        Homeplate = carrier;
    }
    public void BindToFacility(ICarrierCraftHoldingFacility facility)
    {
        CurrentHoldingFacility = facility;
        Debug.Log(this + ": Assigned to " + CurrentHoldingFacility.Name + ".");
    }
    public void SetAvailabilityDelay(float delayFromCurrentTimeInSeconds)
    {
        AvailableForHandling = DateTimeModel.CurrentDateTime.AddSeconds(delayFromCurrentTimeInSeconds);
        Debug.Log(this + ": Availability changed to " + AvailableForHandling.ToString("G") + ".");
    }
    public bool IsAvailableForHandling()
    {
        if (IsAlive && DateTimeModel.CurrentDateTime >= AvailableForHandling)
        {
            return true;
        }
        else return false;
    }
    public void SetCarrierOpsState(CarrierOpsState newState)
    {
        CarrierOpsState = newState;
    }

    public void AssignSortie(ISortieData sortie)
    {
        SortieData = sortie;
        IsSortied = true;
    }
    public void ClearSortieData()
    {
        SortieData = null;
        IsSortied = false;
    }
}