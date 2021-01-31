using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingResultData
{
    public Vector2 targetPoint { get; private set; }
    public List<UnitIdentifier> unitIdentifiers { get; private set; }

    public TargetingResultData(Vector2 targetPoint, List<UnitIdentifier> unitIdentifiers)
    {
        this.targetPoint = targetPoint;
        this.unitIdentifiers = unitIdentifiers;
    }
}
