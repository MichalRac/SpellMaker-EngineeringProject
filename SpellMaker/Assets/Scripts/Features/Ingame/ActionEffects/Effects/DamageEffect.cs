using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : ActionEffect
{
    public DamageEffect(ActionEffectData abilitySetupSO) : base(abilitySetupSO)
    {
    }

    public DamageEffect(int turnLenght, int power) : base(turnLenght, power)
    {
    }

    public override void Affect(BaseCharacterMaster unitAffected, bool decrementTurnsLeft)
    {
        base.Affect(unitAffected, decrementTurnsLeft);
        unitAffected.ReciveDamage(Power);
    }
}
