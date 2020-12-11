using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitCommand : IUnitCommand
{
    private Vector3 target;

    public MoveUnitCommand(BaseCharacterMaster activeCharacter, Vector3 target) : base(activeCharacter) 
    {
        this.target = target;
    }

    public override void Execute(Action onCommandFinished)
    {
        activeCharacter.TriggerMoveToEmpty(target, onCommandFinished);
    }
}
