using UnityEngine;
/// <summary>
/// This is a one-shot command that ASKS craft's Flight Control to add it to CraftToLand list.
/// Success not guaranteed!
/// </summary>
public class LandCraft : CommandWithCoroutine, ICommand
{
    private readonly ICarrierCapable craft;
    private readonly IFlightControl flightControl;
    private bool didWeSendTheRequestToFlightControl = false;

    public LandCraft(IFleetCommandHandler commandable, ICarrierCapable craft, IFlightControl newFlightControl,
        bool isInstant)
    {
        IsInstant = isInstant;
        Commandable = commandable;
        this.craft = craft;
        flightControl = newFlightControl;
        Commandable.AddCommand(this);
    }
    public override bool IsFailed()
    {
        if (craft == null || !craft.IsAlive || flightControl == null || !flightControl.IsOperational)
        {
            Debug.Log(this + ": FAILURE!");
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
        flightControl.AddCraftToLand(craft);
        didWeSendTheRequestToFlightControl = true;
    }
}
