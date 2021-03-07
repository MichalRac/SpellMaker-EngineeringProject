using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingResultData
{
    public Vector3 targetPoint { get; private set; }
    public List<UnitIdentifier> unitIdentifiers { get; private set; }

    public TargetingResultData(Vector3 targetPoint, List<UnitIdentifier> unitIdentifiers)
    {
        targetPoint.y = 0f;
        this.targetPoint = targetPoint;
        this.unitIdentifiers = unitIdentifiers;
    }
}
