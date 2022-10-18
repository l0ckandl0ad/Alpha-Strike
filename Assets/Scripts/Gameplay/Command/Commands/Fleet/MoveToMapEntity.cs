using UnityEngine;

public class MoveToMapEntity : MoveToDynamicPosition, ICommand, IWaypointOwner
{
    public MoveToMapEntity(IFleetCommandHandler commandable, IMapEntity targetEntity)
    {
        SetTarget(targetEntity, targetEntity.Transform);
        Initialize(commandable, GetCurrentDestination());
    }
    protected override Vector2 GetCurrentDestination()
    {
        return targetTransform.position;
    }
    public override bool IsConditionMet()
    {
        if (Vector2.Distance(transform.position, Destination) <= 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
