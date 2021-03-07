using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntEffect : ActionEffect
{
    public BaseCharacterMaster tauntTarget { get; set; }

    public TauntEffect(ActionEffectData actionEffectData, BaseCharacterMaster tauntTarget) : base(actionEffectData)
    {
        this.tauntTarget = tauntTarget;
    }

    public TauntEffect(int turnLenght, int power, BaseCharacterMaster tauntTarget) : base(turnLenght, power)
    {
        this.tauntTarget = tauntTarget;
    }

    public override void Affect(BaseCharacterMaster unitAffected, bool decrementTurnsLeft)
    {
        base.Affect(unitAffected, decrementTurnsLeft);
        unitAffected.RefreshLabel();
    }
}
