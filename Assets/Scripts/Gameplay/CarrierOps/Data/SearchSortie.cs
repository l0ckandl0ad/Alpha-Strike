[System.Serializable]
public struct SearchSortie : ISortieData
{
    private float distance;
    private float angleInDegrees;

    public SearchSortie(float distance, float angleInDegrees)
    {
        this.distance = distance;
        this.angleInDegrees = angleInDegrees;
    }

    public void GetCommands(ICarrierCapable craft)
    {
        if (craft.MapEntity.GameObject.TryGetComponent(out IFleetCommandHandler craftCommander))
        {
            if (craft.Homeplate.MapEntity != null)
            {
                ICarrier carrier = craft.Homeplate;
                if (carrier.IsAlive)
                {
                    new MoveToOffsetVector(craftCommander, carrier.MapEntity, distance, angleInDegrees);

                    //new MoveToMapEntity(craftCommander, carrier.MapEntity);
                    //new LandCraft(craftCommander, craft, craft.CurrentHoldingFacility.FlightControl, false);
                    
                    new ReturnToCarrier(craft);
                }
            }
        }
    }
}
