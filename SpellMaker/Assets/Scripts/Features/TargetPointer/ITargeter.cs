using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    List<BaseCharacterMaster> GetTargets();
    void Setup(AbilitySize abilitySize);
    void Move(Vector3 displacement);
}
