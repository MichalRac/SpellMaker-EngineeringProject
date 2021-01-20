using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbility : MonoBehaviour
{
    public TargetingType TargetingType { get; private set; }
    public AbilitySize AbilitySize { get; private set; }
    public List<ActionEffect> AbilityEffects { get; private set; }

    public UnitAbility(TargetingType targetingType, AbilitySize abilitySize, List<ActionEffect> abilityEffects)
    {
        TargetingType = targetingType;
        AbilitySize = abilitySize;
        AbilityEffects = abilityEffects;
    }

}

public static class UnitAbilityFactory
{
    public static UnitAbility GetUnitAbility(AbilitySetupSO abilitySetupSO)
    {

        List<ActionEffect> actionEffects = new List<ActionEffect>();
        foreach(var actionEffectData in abilitySetupSO.AbilityEffects)
        {
            actionEffects.Add(ActionEffectFactory.GetActionEffect(actionEffectData));
        }

        return new UnitAbility(abilitySetupSO.TargetingType, abilitySetupSO.AbilitySize, actionEffects);
    }

    public static List<UnitAbility> GetUnitAbilities(UnitDataSO unitDataSO)
    {
        List<UnitAbility> unitAbilities = new List<UnitAbility>();

        foreach(var abilitySetup in unitDataSO.abilities)
        {
            unitAbilities.Add(GetUnitAbility(abilitySetup));
        }

        return unitAbilities;
    }
}