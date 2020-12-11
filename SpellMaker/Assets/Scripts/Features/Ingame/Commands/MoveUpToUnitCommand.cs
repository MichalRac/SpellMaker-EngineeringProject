using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpToUnitCommand : IUnitCommand
{
    private IUnit targetUnit;
    public MoveUpToUnitCommand(BaseCharacterMaster activeCharacter, IUnit targetUnit) : base(activeCharacter)
    {
        this.targetUnit = targetUnit;
    }

    public override void Execute(Action onCommandFinished)
    {
        activeCharacter.TriggerMoveToUnit(targetUnit, onCommandFinished);
    }


}
