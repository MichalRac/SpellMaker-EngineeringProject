using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    void Setup(int currentTeamId, Vector3 initPos, AbilitySize abilitySize, List<UnitRelativeOwner> targetGroup);
    TargetingResultData ExecuteTargeting();
    void CancelTargeting();
    void Move(Vector3 displacement);
}
