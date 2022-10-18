using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a one-shot command that ASKS craft's Flight Control to add it to CraftToLaunch list.
/// Success not guaranteed!
/// </summary>
public class LaunchCraft : CommandWithCoroutine, ICommand
{
    private readonly ICarrierCapable craft;
    private readonly IFlightControl flightControl;
    private bool didWeSendTheRequestToFlightControl = false;

    public LaunchCraft(ICarrierOpsHandler commandable, ICarrierCapable craft, bool isInstant)
    {
        IsInstant = isInstant;
        Commandable = commandable;
        this.craft = craft;
        flightControl = craft.CurrentHoldingFacility.FlightControl;
        Commandable.AddCommand(this);
    }
    public override bool IsFailed()
    {
        if (craft == null || !craft.IsAlive || !craft.CurrentHoldingFacility.IsOperational)
        {
            return true;
        }
        else return false;
    }
    public override bool IsConditionMet()
    {
        if (didWeSendTheRequestToFlightControl)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void DoOneFrameOfCommand()
    {
        flightControl.AddCraftToLaunch(craft);
        didWeSendTheRequestToFlightControl = true;
    }
}
