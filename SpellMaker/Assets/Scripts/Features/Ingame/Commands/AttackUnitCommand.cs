using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitCommand : IUnitCommand
{
    private IUnit targetUnit;

    public AttackUnitCommand(BaseCharacterMaster activeCharacter, IUnit targetUnit) : base(activeCharacter)
    {
        this.targetUnit = targetUnit;
    }

    public override void Execute(Action onCommandFinished)
    {
        activeCharacter.TriggerAttackAnim(onCommandFinished);

        if(targetUnit is BaseCharacterMaster targetCharacterMaster)
        {
            targetCharacterMaster.TriggerDamagedAnim(null);
            targetCharacterMaster.ReciveDamage(20);
        }
    }
}
