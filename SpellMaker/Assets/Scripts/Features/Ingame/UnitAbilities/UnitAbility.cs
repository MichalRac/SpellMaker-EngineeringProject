using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class UnitAbility
{
    public string AbilityName { get; private set; }
    public bool Independant { get; private set; }
    public TargetingType TargetingType { get; private set; }
    public float AbilitySize { get; private set; }
    public List<ActionEffect> AbilityEffects { get; private set; }
    public Queue<AbstractUnitCommand> AbilityCommandQueue { get; private set; }
    public List<UnitRelativeOwner> TargetGroup { get; private set; }

    public UnitAbility(string abilityName, bool independant, TargetingType targetingType, float abilitySize, List<ActionEffect> abilityEffects, List<AbstractUnitCommand> commands, List<UnitRelativeOwner> AffectedCharacters)
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

    public List<(CommonCommandData commonCommandData, OptionalCommandData optionalCommandData)> GetPossibleAbilityUses(Unit actor, WorldModel worldModelLayer)
    {
        
        List<(CommonCommandData commonCommandData, OptionalCommandData optionalCommandData)> PossibleAbilityUses = 
            new List<(CommonCommandData commonCommandData, OptionalCommandData optionalCommandData)>();

        // TODO Have AI consider changing it's position to more optimal
        if (AbilityName == "Walk")
            return PossibleAbilityUses;

        switch (TargetingType)
        {
            case TargetingType.Single:
                var fittingUnits = worldModelLayer.GetUnitsFittingRequirement(actor, TargetGroup);
                foreach (var fittingUnit in fittingUnits)
                {
                    PossibleAbilityUses.Add( (
                            new CommonCommandData(actor.UnitIdentifier, new List<UnitIdentifier> {fittingUnit.UnitIdentifier}, this, null), 
                            new OptionalCommandData(Vector3.zero) )
                        );
                }
                break;
            case TargetingType.Line:
                fittingUnits = worldModelLayer.GetUnitsFittingRequirement(actor, TargetGroup);
                for (int i = 0; i < fittingUnits.Count; i++)
                {
                    var mainTarget = fittingUnits[i].UnitIdentifier;
                    var Targets = new List<UnitIdentifier> { mainTarget };
                    
                    for (int j = 0; j < fittingUnits.Count; j++)
                    {
                        if (i == j) continue;

                        var subTarget = fittingUnits[j].UnitIdentifier;
                        
                        var distanceFromSkillLine = GeneralExtensions.GetDistanceFromLine(fittingUnits[j].UnitData.Position, actor.UnitData.Position, fittingUnits[i].UnitData.Position);
                        if(distanceFromSkillLine - 1f < AbilitySize)
                        {
                            Targets.Add(subTarget);
                        }
                    }
                    
                    PossibleAbilityUses.Add( (
                        new CommonCommandData( actor.UnitIdentifier, Targets, this, null), 
                        new OptionalCommandData( Vector3.zero )
                        ));
                }
                break;
            case TargetingType.Circle:
                fittingUnits = worldModelLayer.GetUnitsFittingRequirement(actor, TargetGroup);
                for (int i = 0; i < fittingUnits.Count; i++)
                {
                    var mainTarget = fittingUnits[i].UnitIdentifier;
                    var Targets = new List<UnitIdentifier> { mainTarget };
                    for (int j = 0; j < fittingUnits.Count; j++)
                    {
                        if (i == j) continue;

                        var subTarget = fittingUnits[j].UnitIdentifier;

                        var distanceFromSkillCenter = Vector3.Distance(fittingUnits[i].UnitData.Position, fittingUnits[j].UnitData.Position);
                        if(distanceFromSkillCenter - 1f < AbilitySize)
                        {
                            Targets.Add(subTarget);
                        }
                    }
                    
                    PossibleAbilityUses.Add( (
                        new CommonCommandData( actor.UnitIdentifier, Targets, this, null), 
                        new OptionalCommandData( Vector3.zero )
                    ));
                }
                break;
            case TargetingType.Self:
                PossibleAbilityUses.Add( (
                        new CommonCommandData(actor.UnitIdentifier, new List<UnitIdentifier>{actor.UnitIdentifier}, this, null), 
                        new OptionalCommandData(Vector3.zero))
                );
                break;
            case TargetingType.All:
                fittingUnits = worldModelLayer.GetUnitsFittingRequirement(actor, TargetGroup);
                var unitIdentifiers = fittingUnits.Select(fittingUnit => fittingUnit.UnitIdentifier).ToList();
                PossibleAbilityUses.Add( (
                    new CommonCommandData(actor.UnitIdentifier, unitIdentifiers, this, null), 
                    new OptionalCommandData(Vector3.zero))
                    );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return PossibleAbilityUses;
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

        return new UnitAbility(abilitySetupSO.AbilityName, abilitySetupSO.Independant, abilitySetupSO.TargetingType, abilitySetupSO.TargeterScaleSo.GetAbilityTargeterScale(abilitySetupSO.AbilitySize), actionEffects, abilitySetupSO.CommandQueue, abilitySetupSO.AffectedCharacters);
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