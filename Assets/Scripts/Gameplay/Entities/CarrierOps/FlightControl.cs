using AlphaStrike.Gameplay.CarrierOps;
using System;
using System.Collections.Generic;

[System.Serializable]
public class FlightControl : Module, IFlightControl
{
    private bool isLaunchOpsAllowed = true; // may be disallowed to provide space for landing more craft 

    public int MaxNumberOfCraftAllowedTotal { get; protected set; }
    public List<ICarrierCapable> CraftToReady { get; protected set; } = new List<ICarrierCapable>();
    public List<ICarrierCapable> CraftToStandDown { get; protected set; } = new List<ICarrierCapable>();
    public List<ICarrierCapable> CraftToLand { get; protected set; } = new List<ICarrierCapable>();
    public List<ICarrierCapable> CraftToLaunch { get; protected set; } = new List<ICarrierCapable>();
    public ICarrierCraftHoldingFacility Hangar { get; protected set; }
    public ICarrierCraftHoldingFacility TransferArea { get; protected set; }
    public IFlightDeck FlightDeck { get; protected set; }
    [field: NonSerialized]
    public ICarrierOpsHandler CurrentCarrierCommandHandler { get; protected set; }
    public FlightControl(ICarrierCraftHoldingFacility hangar, ICarrierCraftHoldingFacility transferArea,
        IFlightDeck flightDeck)
    {
        Name = "Flight Control";
        Type = ModuleType.FlightControl;
        
        Hangar = hangar;
        TransferArea = transferArea;
        FlightDeck = flightDeck;

        Hangar.BindToFlightControl(this);
        TransferArea.BindToFlightControl(this);
        FlightDeck.BindToFlightControl(this);

        MaxNumberOfCraftAllowedTotal = Hangar.MaxNumberOfCraftAllowed + (FlightDeck.MaxNumberOfCraftAllowed / 2);
    }
    private bool AddCraftToList(ICarrierCapable craft, List<ICarrierCapable> list)
    {
        if (list.Contains(craft) || !craft.IsAlive) return false;
        list.Add(craft);
        return true;
    }
    private bool RemoveCraftFromList(ICarrierCapable craft, List<ICarrierCapable> list)
    {
        if (list.Contains(craft))
        {
            list.Remove(craft);
            return true;
        }
        else return false;
    }
    public bool AddCraftToReady(ICarrierCapable craft)
    {
        return AddCraftToList(craft, CraftToReady);
    }
    public bool AddCraftToStandDown(ICarrierCapable craft)
    {
        return AddCraftToList(craft, CraftToStandDown);
    }
    public bool AddCraftToLand(ICarrierCapable craft)
    {
        bool result = false;
        // See if we can handle landing another craft here.
        int currentNumberOfCraftOnBoard = Hangar.CarrierCraftList.Count + TransferArea.CarrierCraftList.Count
            + FlightDeck.CarrierCraftList.Count;
        if (currentNumberOfCraftOnBoard + CraftToLand.Count + 1 <= MaxNumberOfCraftAllowedTotal)
        {
            if (CarrierOpsUtils.IsWithinLandingDistance(CurrentCarrierCommandHandler.MapEntity, craft.MapEntity))
            {
                return AddCraftToList(craft, CraftToLand);
            }
        }
        return result;
    }
    public bool AddCraftToLaunch(ICarrierCapable craft)
    {
        return AddCraftToList(craft, CraftToLaunch);
    }

    /// <summary>
    /// Change state of operations for this craft and send it to hangar if found wondering about in transfer area.
    /// </summary>
    private bool ChangeCraftState(ICarrierCapable craft, CarrierOpsState state)
    {
        if (craft.CurrentHoldingFacility.CanReadyCraft)
        {
            craft.CurrentHoldingFacility.ChangeReadiness(craft, state); // ready craft if you can
            return true;
        }
        else
        {   // or check the transfer area
            if ((state != CarrierOpsState.InFlight) && TransferArea.CarrierCraftList.Contains(craft)) 
            {
                TransferArea.TransferCraftToFacility(craft, Hangar); // and send it to hangar to try again next time
            }
            return false;
        }
    }
    /// <summary>
    /// Unready the craft and send it to hangar.
    /// </summary>
    private bool StandDownCraft(ICarrierCapable craft)
    {
        craft.ClearSortieData(); // just in case...

        if (craft.CarrierOpsState == CarrierOpsState.Unready)
        {
            if (craft.CurrentHoldingFacility == FlightDeck) // if on flight deck, go to transfer area
            {
                FlightDeck.TransferCraftToFacility(craft, TransferArea);
                return false;
            }
            if (craft.CurrentHoldingFacility == TransferArea) // then to hangar area
            {
                TransferArea.TransferCraftToFacility(craft, Hangar);
                return false;
            }
            if (craft.CurrentHoldingFacility == Hangar) // end here and return true!
            {
                return true;
            }
            return false;
        }
        else // if in other state, then start the unreadying process and start over
        {
            ChangeCraftState(craft, CarrierOpsState.Unready);
            return false;
        }
    }
    private bool LandCraft(ICarrierCapable craft)
    {
        return FlightDeck.LandCraft(craft);
    }
    private bool LaunchCraft(ICarrierCapable craft)
    {
        if (craft.CarrierOpsState != CarrierOpsState.Ready) 
        {
            ChangeCraftState(craft, CarrierOpsState.Ready); // ready if not ready
            return false;
        }
        else
        {
            if (craft.CurrentHoldingFacility == FlightDeck)  // if ready on flight deck
            {
                return FlightDeck.LaunchCraft(craft); // then launch
            }
            if (craft.CurrentHoldingFacility == TransferArea) // or guide to flightdeck via transfer area
            {
                TransferArea.TransferCraftToFacility(craft, FlightDeck);
                return false;
            }
            if (craft.CurrentHoldingFacility == Hangar) // or from hangar to transfer area
            {
                Hangar.TransferCraftToFacility(craft, TransferArea);
                return false;
            }
            return false;
        }
    }
    public void RunOperations(ICarrierOpsHandler currentCarrierCommandHandler)
    {   // DO OPS HERE!
        if (!IsOperational) return; // exit if this facility is non-operational

        CurrentCarrierCommandHandler = currentCarrierCommandHandler; // monob who runs this method

        RunReadyOps();
        RunStandDownOps();
        RunLandingOps();
        if (isLaunchOpsAllowed) RunLaunchOps();
    }

    private void RunReadyOps()
    {
        if (CraftToReady.Count > 0)
        {
            // get temp list to allow processing it via foreach
            List<ICarrierCapable> tempList = new List<ICarrierCapable>(CraftToReady);
            foreach (ICarrierCapable craft in tempList)
            {
                if (ChangeCraftState(craft, CarrierOpsState.Ready) || !craft.IsAlive) //try readying the craft
                {
                    RemoveCraftFromList(craft, CraftToReady); // remove from the list if succeed
                }
            }
        }
    }
    private void RunStandDownOps()
    {
        if (CraftToStandDown.Count > 0)
        {
            // get temp list to allow processing it via foreach
            List<ICarrierCapable> tempList = new List<ICarrierCapable>(CraftToStandDown);
            foreach (ICarrierCapable craft in tempList)
            {
                if (StandDownCraft(craft) || !craft.IsAlive) //try standing down the craft
                {
                    RemoveCraftFromList(craft, CraftToStandDown); // remove from the list if succeed
                }
            }
        }
    }
    private void RunLandingOps()
    {
        if (CraftToLand.Count > 0)
        {
            // SEE IF THERE'S ENOUGH ROOM AND MAKE SOME IF NEEDED
            HaltLaunchOpsProcedure();

            // get temp list to allow processing it via foreach
            List<ICarrierCapable> tempList = new List<ICarrierCapable>(CraftToLand);
            foreach (ICarrierCapable craft in tempList)
            {
                LandCraft(craft); // attempt landing and remove from the list, regardless of result
                RemoveCraftFromList(craft, CraftToLand);
            }
        }
    }

    private void HaltLaunchOpsProcedure()
    {
        // see if we need to halt launch ops
        if (FlightDeck.CarrierCraftList.Count >= FlightDeck.MaxNumberOfCraftAllowed - 4) // TEMP! USE CONST FROM SMWRE
        {
            isLaunchOpsAllowed = false;
        }
        else isLaunchOpsAllowed = true;
    }

    private void RunLaunchOps()
    {
        if (CraftToLaunch.Count > 0)
        {
            // get temp list to allow processing it via foreach
            List<ICarrierCapable> tempList = new List<ICarrierCapable>(CraftToLaunch);
            foreach (ICarrierCapable craft in tempList)
            {
                if (LaunchCraft(craft) || !craft.IsAlive) //try launching the craft
                {
                    RemoveCraftFromList(craft, CraftToLaunch); // remove from the list if succeed
                }
            }
        }
    }
}
