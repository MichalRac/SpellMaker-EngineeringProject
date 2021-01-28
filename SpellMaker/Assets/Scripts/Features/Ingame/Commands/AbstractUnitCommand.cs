using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitCommand : ScriptableObject, ICommand
{
    public abstract void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null);
}

public class CommonCommandData
{
    public BaseCharacterMaster actor;
    public List<BaseCharacterMaster> targets;
    public Action onCommandCompletedCallback;

    public CommonCommandData(BaseCharacterMaster actor, List<BaseCharacterMaster> targets, Action onCommandCompletedCallback)
    {
        this.actor = actor;
        this.targets = targets;
        this.onCommandCompletedCallback = onCommandCompletedCallback;
    }
}

public class OptionalCommandData
{
    public Vector3 position { get; private set; }
}