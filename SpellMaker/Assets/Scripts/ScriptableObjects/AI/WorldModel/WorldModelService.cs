using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldModelService
{
    private static readonly int DEFAULT_AI_DEPTH = 4;
    
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

    public void Initialize(List<Unit> unitCopies, Queue<UnitIdentifier> queue, UnitIdentifier activeUnit)
    {
        WorldModelStack = new Stack<WorldModel>();
        WorldModelStack.Push(new WorldModel(unitCopies, queue, activeUnit));
    }

    public TurnAction CalculateAIAction(Unit unit, int AIDepth = -1)
    {
        var rootWorldModel = GetCurrentWorldModelLayer();
        var currentWorldModel = new WorldModel(rootWorldModel.ModelActiveCharacters, rootWorldModel.Queue, rootWorldModel.CurrentlyActiveUnit);
        WorldModelStack.Push(currentWorldModel);

        if (AIDepth == -1)
        {
            AIDepth = DEFAULT_AI_DEPTH;
        }
        AIDepth *= WorldModelStack.Peek().ModelActiveCharacters.Count;
        int currentDepth = 0;

        var possibleActions = GetAllViableActions(unit);

        Stack<TurnAction> currentActionSequence = new Stack<TurnAction>();
        TurnAction currentInitialMove = null;
        var bestAction = possibleActions[0];
        var bestDiscontentmentValue = float.MaxValue;

        do
        {
            if (WorldModelStack.Count >= AIDepth)
            {
                var currentDiscontentment = currentWorldModel.GetDiscontentmentForUnit(unit);

                if (currentDiscontentment < bestDiscontentmentValue)
                {
                    bestAction = currentInitialMove;
                    bestDiscontentmentValue = currentDiscontentment;
                }

                WorldModelStack.Pop();
                currentWorldModel = WorldModelStack.Peek();
            }
            else
            {
                var nextActionEnumerator = currentWorldModel.GetNextActionEnumerator();

                if (nextActionEnumerator.MoveNext())
                {
                    if (currentActionSequence.Count == 0)
                    {
                        currentInitialMove = nextActionEnumerator.Current;
                    }

                    currentActionSequence.Push(nextActionEnumerator.Current);

                    var nextQueueState = currentWorldModel.GetNextQueueState();
                    WorldModelStack.Push(new WorldModel(currentWorldModel.CopyUnits(), nextQueueState.Queue, nextQueueState.CurrentlyActiveUnit));
                    currentWorldModel = WorldModelStack.Peek();

                    currentWorldModel.ApplyTurnAction(nextActionEnumerator.Current);
                }
                else
                {
                    nextActionEnumerator.Dispose();
                    currentActionSequence.Pop();
                    WorldModelStack.Pop();
                    currentWorldModel = WorldModelStack.Count > 0 ? WorldModelStack.Peek() : null;
                }
            }
        } while (WorldModelStack.Count >= 1);
        
        /*foreach (var ability in unit.UnitData.UnitAbilities)
        {
            var possibleAbilityUses = ability.GetPossibleAbilityUses(unit, GetCurrentWorldModelLayer());
            
            foreach (var possibleAbilityUse in possibleAbilityUses)
            {
                possibleActions.Add(new TurnAction(ability.AbilityCommandQueue, possibleAbilityUse.commonCommandData, possibleAbilityUse.optionalCommandData));
                /*
                 var potentialTurnAction = new TurnAction(ability.AbilityCommandQueue, possibleAbilityUse.commonCommandData, possibleAbilityUse.optionalCommandData);
                
                var abilitySequenceHandler = new AbilitySequenceHandler(potentialTurnAction, null);
                abilitySequenceHandler.Simulate();
                #1#
                
                //potentialTurnAction.GetGoalChange();

                // Heuretic assumptions for oprimization
                // if(goalChange < 0) continue
            }
        }*/


        return bestAction;
    }
    
    private List<TurnAction> GetAllViableActions(Unit unit)
    {
        var viableActions = new List<TurnAction>();

        foreach (var ability in unit.UnitData.UnitAbilities)
        {
            var possibleAbilityUses = ability.GetPossibleAbilityUses(unit, GetCurrentWorldModelLayer());
            
            foreach (var possibleAbilityUse in possibleAbilityUses)
            {
                viableActions.Add(new TurnAction(ability.AbilityCommandQueue, possibleAbilityUse.commonCommandData, possibleAbilityUse.optionalCommandData));
            }
        }
        
        return viableActions;
    }

    public WorldModel ApplyAction(TurnAction action)
    {
        var abilitySequenceHandler = new AbilitySequenceHandler(action, null);
        return new WorldModel(new List<Unit>(), null, null);
    }
}
