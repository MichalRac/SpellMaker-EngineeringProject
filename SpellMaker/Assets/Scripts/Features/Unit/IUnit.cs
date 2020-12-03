using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    void Initialize(UnitData data);
    void SetSelect(bool value);
    void SetHighlight(bool value);
}

