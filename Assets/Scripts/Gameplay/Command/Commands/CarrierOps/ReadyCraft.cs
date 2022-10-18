using UnityEngine;
/// <summary>
/// This is a one-shot command that does not guarantee that the request will succeed.
/// It only TRIES to request Flight Control responsible for the craft to ready it.
/// Flight Control will TRY to add this request to its own TODO list and then will handle it from there.
/// So this command cannot be terminated once it was called.
/// </summary>
public class ReadyCraft : CommandWithCoroutine, ICommand
{
    private readonly ICarrierCapable craft;
    private readonly IFlightControl flightControl;
    private bool didWeSendTheRequestToFlightControl = false;

    public ReadyCraft(ICarrierOpsHandler commandable, ICarrierCapable craft)
    {
        IsInstant = true;
        Commandable = commandable;
        this.craft = craft;
        flightControl = craft.CurrentHoldingFacility.FlightControl;
        Commandable.AddCommand(this);
    }
    public override bool IsFailed()
    {
        if (craft == null || !craft.IsAlive || craft.CarrierOpsState == CarrierOpsState.InFlight
            || !craft.CurrentHoldingFacility.IsOperational)
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
        flightControl.AddCraftToReady(craft);
        didWeSendTheRequestToFlightControl = true;
    }
}
