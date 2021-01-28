using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackUnitCommand", menuName = "ScriptableObjects/Commands/Damage/AttackUnitCommand")]
public class AttackUnitCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        if (commandData.targets.Count == 0)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with no targets");
            return;
        }

        if (commandData.targets.Count > 1)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with single AttackUnitCommand");
        }

        commandData.actor.TriggerAttackAnim(commandData.onCommandCompletedCallback, () => 
        {
            if (commandData.targets[0] == null)
                return;

            commandData.targets[0].TriggerDamagedAnim(null);
            commandData.targets[0].ReciveDamage(commandData.actor.Unit.unitData.baseDamage);
        });
    }
}
