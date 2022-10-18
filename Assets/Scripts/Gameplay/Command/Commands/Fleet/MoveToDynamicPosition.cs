using UnityEngine;

public abstract class MoveToDynamicPosition : MoveToCommand, ICommand, IWaypointOwner
{
    protected override Vector2 Destination { get => GetCurrentDestination(); }
    protected IMapEntity targetEntity;
    protected Transform targetTransform;

    protected virtual void SetTarget(IMapEntity targetEntity, Transform targetTransform)
    {
        this.targetEntity = targetEntity;
        this.targetTransform = targetEntity.Transform;
    }

    protected abstract Vector2 GetCurrentDestination();

    public override bool IsConditionMet()
    {
        return false;
    }
    public override bool IsFailed()
    {
        if (Commandable.MapEntity == null || targetEntity == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateWaypointPosition()
    {
        prefabWaypointScript.SetPosition(Destination);
    }
    public override void DoOneFrameOfCommand() // one move cycle for the coroutine to execute
    {
        UpdateWaypointPosition();
        base.DoOneFrameOfCommand();
    }
}
