using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbility
{
    public string AbilityName { get; private set; }
    public bool Independant { get; private set; }
    public TargetingType TargetingType { get; private set; }
    public AbilitySize AbilitySize { get; private set; }
    public List<ActionEffect> AbilityEffects { get; private set; }
    public Queue<AbstractUnitCommand> AbilityCommandQueue { get; private set; }
    public List<UnitRelativeOwner> TargetGroup { get; private set; }

    public UnitAbility(string abilityName, bool independant, TargetingType targetingType, AbilitySize abilitySize, List<ActionEffect> abilityEffects, List<AbstractUnitCommand> commands, List<UnitRelativeOwner> AffectedCharacters)
    {
        AbilityName = abilityName;
        Independant = independant;
        TargetingType = targetingType;
        AbilitySize = abilitySize;
        AbilityEffects = abilityEffects;
        AbilityCommandQueue = new Queue<AbstractUnitCommand>();
        foreach (var command in commands)
            AbilityCommandQueue.Enqueue(command);
        this.TargetGroup = AffectedCharacters;
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

        return new UnitAbility(abilitySetupSO.AbilityName, abilitySetupSO.Independant, abilitySetupSO.TargetingType, abilitySetupSO.AbilitySize, actionEffects, abilitySetupSO.CommandQueue, abilitySetupSO.AffectedCharacters);
    }

    public static List<UnitAbility> GetUnitAbilities(UnitDataSO unitDataSO)
    {
        List<UnitAbility> unitAbilities = new List<UnitAbility>();

        foreach(var abilitySetup in unitDataSO.abilities)
        {
            var unitAbility = GetUnitAbility(abilitySetup);
            unitAbilities.Add(unitAbility);
        }

        return unitAbilities;
    }
}