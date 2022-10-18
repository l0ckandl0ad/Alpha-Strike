using UnityEngine;

public class FollowMapEntity : MoveToDynamicPosition, ICommand, IWaypointOwner
{
    public FollowMapEntity(IFleetCommandHandler commandable, IMapEntity targetEntity)
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
        return false;
    }
}
