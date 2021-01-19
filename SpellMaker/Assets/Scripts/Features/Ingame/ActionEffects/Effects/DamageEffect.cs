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

    public override void Affect(BaseCharacterMaster unitAffected)
    {
        base.Affect(unitAffected);
        unitAffected.ReciveDamage(Power);
    }
}
