using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class ActionEffect : IActionEffect
{
    public int TurnsLeftAffected { get; set; }
    public int Power { get; }
    public BaseCharacterMaster OptionalTarget { get; set; }

    protected ActionEffect(int turnLenght, int power)
    {
        TurnsLeftAffected = TurnsLeftAffected;
        Power = power;
    }

    protected ActionEffect(ActionEffectData actionEffectData)
    {
        TurnsLeftAffected = actionEffectData.AbilityLenght;
        Power = actionEffectData.AbilityPower;
    }

    // Happens on turn finished
    public virtual void Affect(BaseCharacterMaster unitAffected)
    {
        TurnsLeftAffected--;
    }

    public bool IsFinished() => TurnsLeftAffected <= 0;

}
