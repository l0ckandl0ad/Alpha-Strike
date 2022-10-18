using AlphaStrike.Gameplay.Utils;
using UnityEngine;

public class MoveToOffsetVector : MoveToDynamicPosition, ICommand, IWaypointOwner
{
    protected float distance;
    protected float angleInDegrees;

    public MoveToOffsetVector(IFleetCommandHandler commandable, IMapEntity targetEntity,
        float distance, float angleInDegrees)
    {
        SetTarget(targetEntity, targetEntity.Transform);

        this.distance = distance;
        this.angleInDegrees = angleInDegrees;

        Initialize(commandable, GetCurrentDestination());
    }

    protected override Vector2 GetCurrentDestination()
    {
        return VectorUtils.VectorWithOffset(targetEntity.Transform.position, distance, angleInDegrees);
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
