using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveUpToUnitCommand", menuName = "ScriptableObjects/Commands/Movement/MoveUpToUnitCommand")]
public class MoveUpToUnitCommand : AbstractUnitCommand
{
    private const float STOP_BEFORE_TARGET_DISTANCE = 2f;

    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
        actor.TriggerMoveToUnit(target, commandData.onCommandCompletedCallback);
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var CurrentWorldModelLayer = WorldModelService.Instance.GetCurrentWorldModelLayer();
        if(CurrentWorldModelLayer.TryGetUnit(commandData.actor, out var actor) && CurrentWorldModelLayer.TryGetUnit(commandData.targetsIdentifiers[0], out var target))
        {
            var deltaPositionNormalized = (actor.UnitData.Position - target.UnitData.Position).normalized;
            actor.UnitData.Position = target.UnitData.Position + deltaPositionNormalized * STOP_BEFORE_TARGET_DISTANCE;
        }
        else
        {
            Debug.LogError($"Couldn't find actor Unit with {commandData.actor} UnitIdentifier or target Unit with {commandData.targetsIdentifiers[0]} UnitIdentifier");
        }
        commandData.onCommandCompletedCallback?.Invoke();
    }
}
