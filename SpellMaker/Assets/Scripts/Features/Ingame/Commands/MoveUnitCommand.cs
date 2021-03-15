using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveUnitCommand", menuName = "ScriptableObjects/Commands/Movement/MoveUnitCommand")]
public class MoveUnitCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        if(optionalCommandData == null || optionalCommandData.Position == null)
        {
            Debug.Log("[MoveUnitCommand] Move unit command requires OptionalCommandData object with position");
        }

        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var targetPosition = optionalCommandData.Position;
        actor.TriggerMoveToEmpty(targetPosition, commandData.onCommandCompletedCallback);
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var CurrentWorldModelLayer = WorldModelService.Instance.GetCurrentWorldModelLayer();
        if (CurrentWorldModelLayer.TryGetUnit(commandData.actor, out var actor))
        {
            actor.UnitData.Position = optionalData.Position;
        }
        else
        {
            Debug.LogError($"Couldn't find actor Unit with {commandData.actor} UnitIdentifier");
        }
    }
}
