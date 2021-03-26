using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitCommand : ScriptableObject, ICommand
{
    public abstract void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null);

    public abstract void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null);
}

public class CommonCommandData
{
    public UnitIdentifier actor;
    public List<UnitIdentifier> targetsIdentifiers;
    public UnitAbility unitAbility;
    public Action onCommandCompletedCallback;

    public CommonCommandData(UnitIdentifier actor, List<UnitIdentifier> targetsIdentifiers, UnitAbility unitAbility, Action onCommandCompletedCallback)
    {
        this.actor = actor;
        this.targetsIdentifiers = targetsIdentifiers;
        this.unitAbility = unitAbility;
        this.onCommandCompletedCallback = onCommandCompletedCallback;
    }
}

public class OptionalCommandData
{
    public Vector3 Position { get; private set; }

    public OptionalCommandData(Vector3 position)
    {
        Position = position;
    }
}