using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class ActionEffect : IActionEffect
{
    public int TurnsLeftAffected { get; set; }
    public int Power { get; }

    protected ActionEffect(int turnLenght, int power)
    {
        TurnsLeftAffected = TurnsLeftAffected;
        Power = power;
    }

    // Happens on turn finished
    public virtual void Affect(Unit unitAffected)
    {
        TurnsLeftAffected--;
    }

    public bool IsFinished() => TurnsLeftAffected <= 0;

}
