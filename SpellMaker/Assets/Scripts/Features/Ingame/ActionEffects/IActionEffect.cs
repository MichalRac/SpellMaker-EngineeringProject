using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionEffect
{
    int TurnsLeftAffected { get; set; }

    int Power { get; }

    void Affect(BaseCharacterMaster unitToAffect, bool decrementTurnsLeft);
    void SimulateAffect(Unit unitToAffect, bool decrementTurnsLeft);
    bool IsFinished();
}
