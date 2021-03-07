using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackUnitCommand", menuName = "ScriptableObjects/Commands/Damage/AttackUnitCommand")]
public class AttackUnitCommand : AbstractUnitCommand
{
    [SerializeField] private bool applyBaseDamage;

    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        if (commandData.targetsIdentifiers.Count == 0)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with no targets");
            return;
        }

        if (commandData.targetsIdentifiers.Count > 1)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with single AttackUnitCommand");
        }

        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        actor.TriggerAttackAnim(null, () => 
        {
            commandData.onCommandCompletedCallback?.Invoke();

            if (commandData.targetsIdentifiers[0] == null)
                return;

            var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
            target.ReciveDamage(applyBaseDamage ? actor.Unit.UnitData.BaseDamage : 0);
        });
    }
}
