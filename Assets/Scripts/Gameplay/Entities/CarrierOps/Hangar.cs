using System.Collections.Generic;

[System.Serializable]
public class Hangar : Module, ICarrierCraftHoldingFacility
{
    public Hangar()
    {
        Name = "Hangar";
        Type = ModuleType.Hangar;
    }
    public List<ICarrierCapable> CarrierCraftList { get; protected set; } = new List<ICarrierCapable>();
    public int MaxNumberOfCraftAllowed { get; protected set; } = 100;
    public int MaxCraftSizeAllowed { get; protected set; } = 20;
    public IFlightControl FlightControl { get; protected set; }
    public float ReadyingDelayInSeconds { get; protected set; } = 30f;
    public float TransferDelayInSeconds { get; protected set; } = 30f;
    public bool CanReadyCraft { get; protected set; } = true;

    public void BindToFlightControl(IFlightControl flightControl)
    {
        FlightControl = flightControl;
    }
    public void TeleportCraftHere(ICarrierCapable craft)
    {
        CarrierCraftList.Add(craft);
        craft.BindToFacility(this);
    }
    public void TeleportCraftHere(List<ICarrierCapable> craftList)
    {
        foreach (ICarrierCapable craft in craftList)
        {
            TeleportCraftHere(craft);
        }
    }
    public void RemoveCraftFromHere(ICarrierCapable craft)
    {
        if (CarrierCraftList.Contains(craft))
        {
            CarrierCraftList.Remove(craft);
        }
    }
    public void RemoveCraftFromHere(List<ICarrierCapable> craftList)
    {
        foreach (ICarrierCapable craft in craftList)
        {
            RemoveCraftFromHere(craft);
        }
    }

    private bool IsAllowedToTransfer(ICarrierCapable craft, ICarrierCraftHoldingFacility newFacility)
    {
        if (craft.IsAvailableForHandling())
        {
            if (newFacility.IsOperational && CarrierCraftList.Contains(craft)
            && newFacility.CarrierCraftList.Count < newFacility.MaxNumberOfCraftAllowed
            && newFacility.MaxCraftSizeAllowed >= craft.HangarSize && !newFacility.CarrierCraftList.Contains(craft))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
    public bool ChangeReadiness(ICarrierCapable craft, CarrierOpsState newReadinessState)
    {
        if (craft.IsAvailableForHandling())
        {
            if (CanReadyCraft && IsOperational && CarrierCraftList.Contains(craft)
                && craft.CarrierOpsState != newReadinessState)
            {
                craft.SetAvailabilityDelay(ReadyingDelayInSeconds);
                craft.SetCarrierOpsState(newReadinessState);
                return true;
            }
            else return false;
        }
        else return false;
    }
    public bool ReadyCraft(ICarrierCapable craft)
    {
        return ChangeReadiness(craft, CarrierOpsState.Ready);
    }
    public bool StandDownCraft(ICarrierCapable craft)
    {
        return ChangeReadiness(craft, CarrierOpsState.Unready);
    }
    public bool TransferCraftToFacility(ICarrierCapable craft, ICarrierCraftHoldingFacility newFacility)
    {
        if (IsAllowedToTransfer(craft, newFacility))
        {
            CarrierCraftList.Remove(craft);
            newFacility.CarrierCraftList.Add(craft);
            craft.BindToFacility(newFacility);
            craft.SetAvailabilityDelay(TransferDelayInSeconds);
            return true;
        }
        else
        {
            return false;
        }
    }
}
