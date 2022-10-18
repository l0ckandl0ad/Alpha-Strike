/// <summary>
/// This is a one-shot command that ASKS craft's Flight Control to add it to CraftToStandDown list.
/// Success not guaranteed!
/// </summary>
public class StandDownCraft : CommandWithCoroutine, ICommand
{
    private readonly ICarrierCapable craft;
    private readonly IFlightControl flightControl;
    private bool didWeSendTheRequestToFlightControl = false;

    public StandDownCraft(ICarrierOpsHandler commandable, ICarrierCapable craft)
    {
        IsInstant = true;
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
        flightControl.AddCraftToStandDown(craft);
        didWeSendTheRequestToFlightControl = true;
    }
}
