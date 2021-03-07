using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveUpToUnitCommand", menuName = "ScriptableObjects/Commands/Movement/MoveUpToUnitCommand")]
public class MoveUpToUnitCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
        actor.TriggerMoveToUnit(target, commandData.onCommandCompletedCallback);
    }
}
