using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveUpToUnitCommand", menuName = "ScriptableObjects/Commands/Movement/MoveUpToUnitCommand")]
public class MoveUpToUnitCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        commandData.actor.TriggerMoveToUnit(commandData.targets[0], commandData.onCommandCompletedCallback);
    }
}
