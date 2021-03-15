using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySetupSO", menuName = "ScriptableObjects/Abilities/AbilitySetupSO", order = 2)]
[System.Serializable]
public class AbilitySetupSO : ScriptableObject
{
    public string AbilityName;
    public string Description;
    public bool Independant;
    public TargetingType TargetingType;
    public AbilitySize AbilitySize;
    public TargeterScaleSO TargeterScaleSo;
    public List<ActionEffectData> AbilityEffects;
    public List<AbstractUnitCommand> CommandQueue;
    public List<UnitRelativeOwner> AffectedCharacters;
}

[System.Serializable]
public class ActionEffectData
{
    public ActionEffectType AbilityEffectType;
    public int AbilityLenght;
    public int AbilityPower;
}
