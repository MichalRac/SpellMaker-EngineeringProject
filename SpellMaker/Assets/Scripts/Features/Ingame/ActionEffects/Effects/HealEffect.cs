using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : ActionEffect
{
    public HealEffect(ActionEffectData abilitySetupSO) : base(abilitySetupSO) { }

    public HealEffect(int turnLenght, int power) : base(turnLenght, power) { }

    public override void Affect(BaseCharacterMaster unitAffected)
    {
        base.Affect(unitAffected);
        unitAffected.ReciveDamage(-Power);
    }
}
