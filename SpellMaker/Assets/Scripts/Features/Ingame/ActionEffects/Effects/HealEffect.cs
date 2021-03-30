using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : ActionEffect
{
    public HealEffect(ActionEffectData abilitySetupSO) : base(abilitySetupSO) { }

    public HealEffect(int turnLenght, int power) : base(turnLenght, power) { }

    public override void Affect(BaseCharacterMaster unitAffected, bool decrementTurnsLeft)
    {
        base.Affect(unitAffected, decrementTurnsLeft);
        unitAffected.ReciveHeal(Power);
    }

    public override void SimulateAffect(Unit unitToAffect, bool decrementTurnsLeft)
    {
        base.SimulateAffect(unitToAffect, decrementTurnsLeft);
        unitToAffect.ApplyHeal(Power);
    }
}
