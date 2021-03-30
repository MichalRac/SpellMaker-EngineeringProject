using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModel
{
    public List<Unit> ModelActiveCharacters { get; }
    public Queue<UnitIdentifier> Queue { get; }
    public UnitIdentifier CurrentlyActiveUnit { get; }


    private List<TurnAction> viableActionsCache;

    private IEnumerator<TurnAction> nextActionEnumeratorCache;
    public IEnumerator<TurnAction> GetNextActionEnumerator() => nextActionEnumeratorCache ?? (nextActionEnumeratorCache = GetNextAction().GetEnumerator());
    public IEnumerable<TurnAction> GetNextAction()
    {
        if (viableActionsCache == null)
            viableActionsCache = GetViableActionsForCurrentlyActiveUnit();

        var index = 0;
        while (index < viableActionsCache.Count)
        {
            yield return viableActionsCache[index++];
        }
    }
    
    public bool IsProcessed { get; set; }

    public WorldModel(List<Unit> modelActiveCharacters, Queue<UnitIdentifier> queue, UnitIdentifier activeUnit)
    {
        ModelActiveCharacters = new List<Unit>(modelActiveCharacters);
        foreach (var modelActiveCharacter in ModelActiveCharacters)
        {
            modelActiveCharacter.SimulateActivateActionEffects();
        }
        Queue = queue;
        CurrentlyActiveUnit = activeUnit;
    }

    public List<Unit> CopyUnits()
    {
        var unitListCopy = new List<Unit>();

        foreach (var modelActiveCharacter in ModelActiveCharacters)
        {
            unitListCopy.Add((Unit)modelActiveCharacter.Clone());
        }

        return unitListCopy;
    }

    public (Queue<UnitIdentifier> Queue, UnitIdentifier CurrentlyActiveUnit) GetNextQueueState()
    {
        var nextQueueState = new Queue<UnitIdentifier>();
        var currentQueueCopy = new Queue<UnitIdentifier>(Queue);

        var nextActive = currentQueueCopy.Peek();
        bool removedFirst = false;
        for (int i = 0; i < currentQueueCopy.Count; i++)
        {
            nextQueueState.Enqueue(currentQueueCopy.Dequeue());

            if (!removedFirst)
            {
                removedFirst = true;
                nextQueueState.Dequeue();
            }
        }
        nextQueueState.Enqueue(CurrentlyActiveUnit);
        
        return (nextQueueState, nextActive);
    }

    private List<TurnAction> GetViableActionsForCurrentlyActiveUnit()
    {
        if (TryGetUnit(CurrentlyActiveUnit, out var unit))
        {
            if (!unit.UnitState.IsAlive)
            {
                return new List<TurnAction>();
            }
            
            return GetAllViableActions(unit);
        }
        
        Debug.Log($"[WorldModel] Trying to get Unit not present in current world model, unitIdentifier: {unit.UnitIdentifier}"); 
        return null;
    }
    
    private List<TurnAction> GetAllViableActions(Unit unit)
    {
        var viableActions = new List<TurnAction>();
        var currentWorldModelLayer = WorldModelService.Instance.GetCurrentWorldModelLayer();

        if (unit.UnitState.IsTaunted(out var tauntEffect))
        {
            var fittingUnits = WorldModelService.Instance.GetCurrentWorldModelLayer().GetUnitsFittingRequirement(unit, new List<UnitRelativeOwner> {UnitRelativeOwner.Opponent});
            var target = tauntEffect.tauntTarget.Unit.UnitState.IsAlive
                ? tauntEffect.tauntTarget.Unit.UnitIdentifier
                : fittingUnits.Count > 0
                    ? fittingUnits[0].UnitIdentifier
                    : null;

            if (target == null)
                return viableActionsCache;

            currentWorldModelLayer.TryGetUnit(target, out var targetUnit);
            
            var basicAttackAbility = unit.UnitData.UnitAbilities.Find((ua) => ua.AbilityName == "Attack");
            viableActions.Add(new TurnAction(
                    basicAttackAbility.AbilityCommandQueue,
                    new CommonCommandData(unit.UnitIdentifier, new List<UnitIdentifier> {target}, basicAttackAbility, null), 
                    new OptionalCommandData(targetUnit.UnitData.Position)
                    ));

            return viableActions;
        }


        foreach (var ability in unit.UnitData.UnitAbilities)
        {
            var possibleAbilityUses = ability.GetPossibleAbilityUses(unit, currentWorldModelLayer);
            
            foreach (var possibleAbilityUse in possibleAbilityUses)
            {
                viableActions.Add(new TurnAction(ability.AbilityCommandQueue, possibleAbilityUse.commonCommandData, possibleAbilityUse.optionalCommandData));
            }
        }
        
        return viableActions;
    }


    public bool TryGetUnit(UnitIdentifier unitIdentifier, out Unit result)
    {
        result = null;
        foreach(var unit in ModelActiveCharacters)
        {
            if(unit.UnitIdentifier.Equals(unitIdentifier))
            {
                result = unit;
                return true;
            }
        }

        return false;
    }

    public List<Unit> GetUnitsFittingRequirement(Unit actor, List<UnitRelativeOwner> unitRelativeOwners)
    {
        var fittingUnits = new List<Unit>();
        
        foreach (var modelActiveCharacter in ModelActiveCharacters)
        {
            if (!unitRelativeOwners.Contains(UnitHelpers.GetRelativeOwner(actor.UnitIdentifier.TeamId, modelActiveCharacter.UnitIdentifier.TeamId)))
            {
                continue;
            }

            if (!modelActiveCharacter.UnitState.IsAlive)
            {
                continue;
            }
            
            fittingUnits.Add(modelActiveCharacter);
        }
        
        return fittingUnits;
    }

    public void ApplyTurnAction(TurnAction turnAction)
    {
        var actionHandler = new AbilitySequenceHandler(turnAction, null);
        actionHandler.Simulate();
    }

    public float GetDiscontentmentForUnit(Unit unit)
    {
        var discontentment = 0f;

        foreach (var goal in unit.UnitData.Goals)
        {
            discontentment += goal.GetDiscontentmentValue(unit, this);
        }
        
        return discontentment;
    }

    public bool DidTeamWin(int teamId)
    {
        int alliesAlive = 0;
        int opponentsAlive = 0;
        foreach (var modelActiveCharacter in ModelActiveCharacters)
        {
            if (!modelActiveCharacter.UnitState.IsAlive)
                continue;
            
            if (UnitHelpers.GetRelativeOwner(modelActiveCharacter.UnitIdentifier.TeamId, teamId) == UnitRelativeOwner.Self)
            {
                alliesAlive++;
            }
            else
            {
                opponentsAlive++;
            }
        }

        return alliesAlive > 0 && opponentsAlive == 0;
    }
    
    public bool DidTeamLose(int teamId)
    {
        int alliesAlive = 0;
        int opponentsAlive = 0;
        foreach (var modelActiveCharacter in ModelActiveCharacters)
        {
            if (!modelActiveCharacter.UnitState.IsAlive)
                continue;
            
            if (UnitHelpers.GetRelativeOwner(modelActiveCharacter.UnitIdentifier.TeamId, teamId) == UnitRelativeOwner.Self)
            {
                alliesAlive++;
            }
            else
            {
                opponentsAlive++;
            }
        }

        return alliesAlive == 0 && opponentsAlive > 0;
    }
}
