using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySequenceHandler
{
    Queue<AbstractUnitCommand> currentQueue;
    CommonCommandData commonCommandData;
    OptionalCommandData optionalCommandData;
    Action onAbilitySequenceFinished;

    public AbilitySequenceHandler(Queue<AbstractUnitCommand> abilityQueue, CommonCommandData commonCommandData, OptionalCommandData optionalCommandData, Action onAbilitySequenceFinished)
    {
        currentQueue = abilityQueue;
        this.commonCommandData = commonCommandData;
        this.optionalCommandData = optionalCommandData;
        this.onAbilitySequenceFinished = onAbilitySequenceFinished;
    }

    public void Begin()
    {
        commonCommandData.onCommandCompletedCallback += ProcessNextCommand;
        ProcessNextCommand();
    }

    public void ProcessNextCommand()
    {
        if (currentQueue.Count == 0)
        {
            commonCommandData.onCommandCompletedCallback -= ProcessNextCommand;
            onAbilitySequenceFinished?.Invoke();
            return;
        }

        var nextCommand = currentQueue.Dequeue();
        nextCommand.Execute(commonCommandData, optionalCommandData);
    }
}
