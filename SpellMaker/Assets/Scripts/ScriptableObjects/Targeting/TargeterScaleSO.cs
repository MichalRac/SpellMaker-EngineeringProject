using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargeterScaleSO", menuName = "ScriptableObjects/Targeting/TargeterScaleSO", order = 0)]
public class TargeterScaleSO : ScriptableObject
{
    public float scaleXS;
    public float scaleS;
    public float scaleM;
    public float scaleL;
    public float scaleXL;

    public float GetAbilityTargeterScale(AbilitySize abilitySize)
    {
        switch (abilitySize)
        {
            case AbilitySize.XS:
                return scaleXS;
            case AbilitySize.S:
                return scaleS;
            case AbilitySize.M:
                return scaleM;
            case AbilitySize.L:
                return scaleL;
            case AbilitySize.XL:
                return scaleXL;
            default:
                Debug.LogError($"[TargeterScaleSO] Tried to fetch unknown targeter scale {abilitySize}");
                return scaleS;
        }
    }
}

public enum AbilitySize
{
    None = -1,

    XS = 0,
    S = 1,
    M = 2,
    L = 3,
    XL = 4,
}