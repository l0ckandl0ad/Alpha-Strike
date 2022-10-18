using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    /// <summary>
    /// A target the command will be executed by.
    /// </summary>
    ICommandable Commandable { get; }
    /// <summary>
    /// To track command's progress.
    /// </summary>
    CommandState CommandState { get; }
    /// <summary>
    /// Does it have to be executed instantly?
    /// </summary>
    bool IsInstant { get; }
    void Execute();
    /// <summary>
    /// CANCEL the command and REMOVE it from the ICommandable it is assigned to
    /// </summary>
    void Terminate();
    
    // REMINDER NOTE!
    // place Commandable.AddCommand(this); in the command's constructor!
}
