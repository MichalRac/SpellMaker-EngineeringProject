using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue
{
    public Queue<ICommand> enqueuedCommands { get; private set; } = new Queue<ICommand>();

    public void Enqueue(ICommand command)
    {
        enqueuedCommands.Enqueue(command);
    }

    public ICommand Dequeue()
    {
        return enqueuedCommands.Dequeue();
    }

    public void RunCommandQueue(Action commandQueueFinished)
    {

    }
}
