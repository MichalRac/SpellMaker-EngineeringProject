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

        unitAffected.Unit.UnitState.ApplyDamage(Power);
        unitAffected.ReciveDamage(Power);
    }

    public override void SimulateAffect(Unit unitToAffect, bool decrementTurnsLeft)
    {
        base.SimulateAffect(unitToAffect, decrementTurnsLeft);

        unitToAffect.UnitState.ApplyDamage(Power);
    }
}
