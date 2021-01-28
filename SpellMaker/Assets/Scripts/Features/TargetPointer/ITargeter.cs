using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    void Setup(AbilitySize abilitySize);
    List<BaseCharacterMaster> ExecuteTargeting();
    void CancelTargeting();
    void Move(Vector3 displacement);
}
