using UnityEngine;
using UnityEngine.UI;

public abstract class MoveToCommand : CommandWithCoroutine, ICommand, IWaypointOwner
{
    protected virtual Vector2 Destination { get; set; } = Vector2.zero; // desired destination
    protected Transform transform; // target's transform
    protected IMovable movableEntity;
    protected GameObject prefabReference = AssetLibrary.Instance.WaypointPrefab;
    protected GameObject prefab;
    protected Waypoint prefabWaypointScript;
    protected Image waypointImage;

    //public MoveToCommand(IFleetCommandHandler commandable, Vector2 destination)
    //{
    //    Initialize(commandable, destination);
    //}

    protected virtual void Initialize(IFleetCommandHandler commandable, Vector2 destination)
    {
        Commandable = commandable; // cache & setup data
        if (Commandable.MapEntity.GameObject.TryGetComponent(out IMovable movableEntity))
        {
            this.movableEntity = movableEntity;
            transform = movableEntity.Transform;
            
            // temp handling of prefab waypoint
            prefab = Object.Instantiate(prefabReference, destination, Quaternion.identity);
            prefabWaypointScript = prefab.GetComponent<Waypoint>();
            prefabWaypointScript.Initialize(Commandable.MapEntity, this);
            waypointImage = prefab.GetComponentInChildren<Image>();
            waypointImage.transform.position = destination;

            Commandable.AddCommand(this); // pass the command to the target it's going to be run on
            MessageLog.SendMessage("New Order: " + Commandable + " move to " + destination.ToString());
        }
        else
        {
            Debug.LogError(this + ": Can't get IMovable component from ICommandable "
                    + Commandable.ToString() + ". Command cancelled.");
            this.Terminate();
        }
    }

    public virtual void UpdateDestination(Vector2 destination) // if destination shifts after the order was given
    {
        this.Destination = destination;
    }

    public override void Terminate() // cancel the command at any point
    {
        base.Terminate();
        Object.Destroy(prefab);
    }

    public override bool IsFailed()
    {
        return false;
    }
    public override bool IsConditionMet()
    {
        if (Vector2.Distance(transform.position, Destination) == 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void DoOneFrameOfCommand() // one move cycle for the coroutine to execute
    {
        transform.position = Vector2.MoveTowards(transform.position, Destination, movableEntity.Speed * Time.deltaTime);
    }
}
