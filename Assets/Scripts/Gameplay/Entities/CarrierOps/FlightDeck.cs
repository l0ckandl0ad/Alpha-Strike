using AlphaStrike.Gameplay.CarrierOps;
using AlphaStrike.Gameplay.Factories;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlightDeck : Hangar, IFlightDeck
{
    public FlightDeck()
    {
        Name = "Flight Deck";
        Type = ModuleType.FlightDeck;
        MaxNumberOfCraftAllowed = 40;
    }
    public bool LandCraft(ICarrierCapable craft)
    {
        return LandingProcess(craft);
    }
    public bool LaunchCraft(ICarrierCapable craft)// TEMP! NOT FULLY IMPLEMENTED!
    {
        if (craft.IsAvailableForHandling())
        {
            if (CarrierCraftList.Contains(craft))
            {
                craft.SetCarrierOpsState(CarrierOpsState.InFlight);
                CarrierCraftList.Remove(craft);
                CreateFleetForCraft(craft);
                craft.SortieData.GetCommands(craft);
                return true;
            }
            else return false;
        }
        else return false;
    }

    private bool LandingProcess(ICarrierCapable craft)
    {
        bool result = false;
        if (craft.IsAvailableForHandling() && craft.CarrierOpsState == CarrierOpsState.InFlight)
        {
            if (IsOperational && !CarrierCraftList.Contains(craft))
            {
                if (FlightControl.CurrentCarrierCommandHandler.TryGetComponent(out IMapEntity mapEntity))
                {
                    if (CarrierOpsUtils.IsWithinLandingDistance(mapEntity, craft.MapEntity)) // if close enough...
                    {
                        CarrierCraftList.Add(craft);
                        craft.BindToFacility(this);
                        craft.SetAvailabilityDelay(TransferDelayInSeconds);
                        craft.SetCarrierOpsState(CarrierOpsState.Unready);
                        craft?.MapEntity?.RemoveEntity(craft);
                        craft.ClearSortieData();
                        result = true;
                    }
                }
            }
        }
        return result;
    }

    private void CreateFleetForCraft(ICarrierCapable craft) // TEMP! resource usage here! should be refactored!
    {
        if (FlightControl.CurrentCarrierCommandHandler.TryGetComponent(out IMapEntity mapEntity))
        {
            List<IFleetData> fleetDataList = new List<IFleetData>();
            List<IVessel> vesselList = new List<IVessel>();
            vesselList.Add(craft);

            fleetDataList.Add(new FleetData(craft.Name, craft.IFF, vesselList, mapEntity.Transform.position.x,
                mapEntity.Transform.position.y));
            FleetAssembler.AssembleFleets(fleetDataList);
        }
    }
}
