using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public interface ICommandable
{
    List<ICommand> Commands { get; }
    Transform Transform { get;  }
    IMapEntity MapEntity { get; }
    /// <summary>
    /// !WARNING! This MUST be called from Command's constructor, at the very end of it, to make sure constructor
    /// finishes setting up everything before passing the command for the execution.
    /// </summary>
    /// <param name="command"></param>
    void AddCommand(ICommand command);
    void RemoveCommand(ICommand command);
    void RemoveAllCommands();

    // Allow Commandable to handle coroutines. !INHERIT! from MonoBehaviour to get concrete implementation of these:
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
    void StopAllCoroutines();
}