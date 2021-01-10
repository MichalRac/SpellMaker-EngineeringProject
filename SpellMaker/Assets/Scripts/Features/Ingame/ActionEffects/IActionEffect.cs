using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionEffect
{
    int TurnsLeftAffected { get; set; }

    int Power { get; }

    void Affect(Unit unitToAffect);
    bool IsFinished();
}
