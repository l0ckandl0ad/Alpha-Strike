using AlphaStrike.Gameplay.CarrierOps;
using System.Collections.Generic;

[System.Serializable]
public abstract class Carrier : CombatVessel, ICarrier
{
    public List<ICarrierFacility> CarrierFacilities { get; protected set; } = new List<ICarrierFacility>();

    protected override void InternalUpdateOnTakeDamage()
    {
        base.InternalUpdateOnTakeDamage();

        CarrierOpsUtils.RemoveDestroyedCraftFromFacilities(CarrierFacilities);
    }
}
