using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModel
{
    public List<Unit> ModelActiveCharacters { get; private set; }

    public WorldModel(List<Unit> modelActiveCharacters)
    {
        ModelActiveCharacters = modelActiveCharacters;
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
            
            fittingUnits.Add(modelActiveCharacter);
        }
        
        return fittingUnits;
    }
}

public class WorldModelService
{
    public static WorldModelService Instance;

    private Stack<WorldModel> WorldModelStack = new Stack<WorldModel>();
    public WorldModel GetCurrentWorldModelLayer() => WorldModelStack.Peek();


    public WorldModelService()
    {
        if (Instance == null)
        {
            WorldModelStack = new Stack<WorldModel>();
            Instance = this;
        }
        else
        {
            Debug.LogError("[UnitManager] Duplicate WorldModelService instances!");
        }
    }

    public void Initialize(List<Unit> unitCopies)
    {
        WorldModelStack = new Stack<WorldModel>();
        WorldModelStack.Push(new WorldModel(unitCopies));
    }
    
    private List<TurnAction> GetAllViableActions(Unit unit)
    {
        var viableActions = new List<TurnAction>();

        foreach (var ability in unit.UnitData.UnitAbilities)
        {
            viableActions.Add(new TurnAction(ability.AbilityCommandQueue, null, null));
        }
        
        return viableActions;
    }

    public WorldModel ApplyAction(TurnAction action)
    {
        var abilitySequenceHandler = new AbilitySequenceHandler(action, null);
        return new WorldModel(new List<Unit>());
    }
}
