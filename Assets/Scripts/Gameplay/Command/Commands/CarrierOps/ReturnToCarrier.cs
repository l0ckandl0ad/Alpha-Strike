using UnityEngine;

public class ReturnToCarrier : MoveToDynamicPosition, ICommand, IWaypointOwner
{
    private readonly ICarrierCapable craft;
    private readonly ICarrier carrier;
    private IFleetCommandHandler craftCommander;
    public ReturnToCarrier(ICarrierCapable craft)
    {
        this.craft = craft;
        this.carrier = craft.Homeplate;
        TrySettingCarrierAsTargetAndInitialize(craft);
    }

    protected void TrySettingCarrierAsTargetAndInitialize(ICarrierCapable craft)
    {
        if (craft.MapEntity.GameObject.TryGetComponent(out IFleetCommandHandler craftCommander))
        {
            this.craftCommander = craftCommander;

            if (carrier.MapEntity != null && carrier.IsAlive)
            {
                SetTarget(carrier.MapEntity, carrier.MapEntity.Transform);
                Initialize(craftCommander, GetCurrentDestination());
            }
        }
        else
        {
            this.Terminate();
        }
    }

    protected override Vector2 GetCurrentDestination()
    {
        return targetTransform.position;
    }
    public override bool IsFailed()
    {
        if (craft == null || !craft.IsAlive || craft.CarrierOpsState != CarrierOpsState.InFlight
            || !carrier.IsAlive)
        {
            return true;
        }
        else return false;
    }
    public override bool IsConditionMet()
    {
        if (Vector2.Distance(transform.position, Destination) <= 1f)
        {
            if (craft.CarrierOpsState == CarrierOpsState.InFlight)
            {
                new LandCraft(craftCommander, craft, craft.CurrentHoldingFacility.FlightControl, true);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
