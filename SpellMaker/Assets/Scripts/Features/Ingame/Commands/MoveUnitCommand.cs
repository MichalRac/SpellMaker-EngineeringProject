using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveUnitCommand", menuName = "ScriptableObjects/Commands/Movement/MoveUnitCommand")]
public class MoveUnitCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        if(optionalCommandData == null || optionalCommandData.position == null)
        {
            Debug.Log("[MoveUnitCommand] Move unit command requires OptionalCommandData object with position");
        }

        commandData.actor.TriggerMoveToEmpty(optionalCommandData.position, commandData.onCommandCompletedCallback);
    }
}
