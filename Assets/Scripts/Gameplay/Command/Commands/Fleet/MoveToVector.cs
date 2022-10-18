using UnityEngine;

public class MoveToVector : MoveToCommand, ICommand, IWaypointOwner
{
    public MoveToVector(IFleetCommandHandler commandable, Vector2 destination)
    {
        this.Destination = destination;
        Initialize(commandable, destination);
    }
}
