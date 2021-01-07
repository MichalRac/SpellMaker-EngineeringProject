using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    void Initialize(Unit data);
    void SetSelect(bool value);
    void SetHighlight(bool value);
    Transform GetTransform();
}

