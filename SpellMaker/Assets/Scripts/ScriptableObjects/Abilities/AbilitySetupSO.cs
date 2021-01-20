using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySetupSO", menuName = "ScriptableObjects/Abilities/AbilitySetupSO", order = 2)]
[System.Serializable]
public class AbilitySetupSO : ScriptableObject
{
    public string AbilityName;
    public TargetingType TargetingType;
    public AbilitySize AbilitySize;
    public List<ActionEffectData> AbilityEffects;
}

[System.Serializable]
public class ActionEffectData
{
    public ActionEffectType AbilityEffectType;
    public int AbilityLenght;
    public int AbilityPower;
}
