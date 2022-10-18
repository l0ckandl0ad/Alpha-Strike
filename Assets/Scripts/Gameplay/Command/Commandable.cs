using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to drive the COMMAND system.
/// </summary>
/// <remarks>
/// It should be assigned to GameObject with IMapEntity on it (ie fleet).
/// </remarks>
public class Commandable : MonoBehaviour, ICommandable
{
    public List<ICommand> Commands { get; protected set; } = new List<ICommand>();
    public Transform Transform { get => transform; }
    public IMapEntity MapEntity { get; protected set; }

    protected virtual void Awake()
    {
        CacheReferences();
    }

    protected virtual void OnEnable()
    {
        CacheReferences();
    }

    protected virtual void CacheReferences()
    {
        if (TryGetComponent(out IMapEntity mapEntity))
        {
            MapEntity = mapEntity;
        }
        else
        {
            Debug.LogError(this + ": MapEntity not found on " + gameObject.ToString() + " gameObject.");
        }
    }

    public void AddCommand(ICommand command)
    {
        if (command.CommandState == CommandState.READY && !Commands.Contains(command))
        {
            if (command.IsInstant)
            {
                command.Execute();
            }
            Commands.Add(command);
        }
    }
    public void RemoveCommand(ICommand command)
    {
        if (Commands.Contains(command))
        {
            Commands.Remove(command);
        }
    }
    public void RemoveAllCommands()
    {
        if (Commands.Count > 0)
        {
            // use temp list to avoid changing the Commands list while it's being processed
            List<ICommand> tempCommands = new List<ICommand>(Commands);
            foreach (ICommand command in tempCommands)
            {
                command.Terminate();
            }
        }
        Commands.Clear();   // just in case we've missed anything somehow
        StopAllCoroutines();
    }
    private void ProcessCommands()
    {
        if (Commands.Count > 0)
        {
            ProcessCommand(Commands[0]);
        }
    }
    private void ProcessCommand(ICommand command)
    {
        switch (command.CommandState)
        {
            case CommandState.READY:
                command.Execute();
                break;
            case CommandState.RUNNING:
                // don't do anything
                break;
            case CommandState.COMPLETED:
                RemoveCommand(command);
                break;
            default:
                // don't do anything
                break;
        }
    }
    protected virtual void Update()
    {
        ProcessCommands();
    }
    protected virtual void OnDestroy()
    {
        RemoveAllCommands();
    }
}
