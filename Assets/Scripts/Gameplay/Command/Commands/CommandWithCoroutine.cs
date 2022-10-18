using System.Collections;
using UnityEngine;

public abstract class CommandWithCoroutine : ICommand
{
    public ICommandable Commandable { get; protected set; }
    public CommandState CommandState { get; protected set; } = CommandState.READY;
    public bool IsInstant { get; protected set; } = false;
    public Coroutine CoroutineInProgress { get; protected set; }

    public virtual void Terminate()
    {
        CommandState = CommandState.COMPLETED; // mark it as completed
        if (Commandable != null)
        {
            Commandable.RemoveCommand(this);
        }
        if (CoroutineInProgress != null)
        {
            Commandable.StopCoroutine(CoroutineInProgress); // stop the coroutine if any
        }
    }
    /// <summary>
    /// Define conditions under which the command FAILS so it can be terminated.
    /// </summary>
    /// <returns></returns>
    public abstract bool IsFailed();
    /// <summary>
    /// Define command's end goal condition here and return TRUE if it's met.
    /// </summary>
    /// <returns></returns>
    public abstract bool IsConditionMet();
    /// <summary>
    /// Command's coroutine will execute this method each frame until the end goal condition is met.
    /// </summary>
    public abstract void DoOneFrameOfCommand();

    public virtual IEnumerator ExecuteCoroutine()
    {
        while (CommandState != CommandState.COMPLETED)
        {
            if (IsFailed() || IsConditionMet())
            {
                Terminate();
                yield break; //stop the coroutine!
            }
            else
            {
                if (!DateTimeModel.IsPaused)
                {
                    DoOneFrameOfCommand();
                }
            }
            yield return null;
        }
    }
    public virtual void Execute()
    {
        if (CommandState == CommandState.READY && Commandable != null)
        {
            CommandState = CommandState.RUNNING;
            CoroutineInProgress = Commandable.StartCoroutine(ExecuteCoroutine());
        }
    }
}
