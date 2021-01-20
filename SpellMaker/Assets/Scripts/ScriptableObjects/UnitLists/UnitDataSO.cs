using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDataSO", menuName = "ScriptableObjects/Units/UnitDataSO", order = 1)]
[System.Serializable]
public class UnitDataSO : ScriptableObject
{
    public UnitClass unitClass;
    public UnitClassMaster unitClassMaster;

    public int health = 50;
    public int baseDamage = 30;

    public List<AbilitySetupSO> abilities;
}
