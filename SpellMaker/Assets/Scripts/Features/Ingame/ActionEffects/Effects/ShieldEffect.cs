using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : ActionEffect
{
    public ShieldEffect(ActionEffectData actionEffectData) : base(actionEffectData)
    {

    }

    public ShieldEffect(int turnLenght, int power) : base(turnLenght, power)
    {

    }

    public override void Affect(BaseCharacterMaster unitAffected, bool decrementTurnsLeft)
    {
        base.Affect(unitAffected, decrementTurnsLeft);

        unitAffected.ToggleShieldVisuals(true);
    }
    
    public override void OnRemoved(BaseCharacterMaster unitAffected)
    {
        base.OnRemoved(unitAffected);

        unitAffected.ToggleShieldVisuals(false);
    }
}
