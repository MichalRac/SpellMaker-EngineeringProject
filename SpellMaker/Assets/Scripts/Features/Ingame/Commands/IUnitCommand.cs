using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnitCommand : ICommand
{
    protected BaseCharacterMaster activeCharacter;

    protected IUnitCommand(BaseCharacterMaster activeCharacter)
    {
        this.activeCharacter = activeCharacter;
    }

    public abstract void Execute(Action onCommandFinished);
}
