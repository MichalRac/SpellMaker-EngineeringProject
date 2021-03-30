using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldModelService
{
    private static readonly int DEFAULT_AI_DEPTH = 6;
    
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

    public TurnAction WIP_CalculateGOAPAction(Unit unit, int AIDepth = -1)
    {
        var rootWorldModel = GetCurrentWorldModelLayer();
        var currentWorldModel = new WorldModel(rootWorldModel.ModelActiveCharacters, rootWorldModel.Queue, rootWorldModel.CurrentlyActiveUnit);
        WorldModelStack.Push(currentWorldModel);

        if (AIDepth == -1)
        {
            AIDepth = DEFAULT_AI_DEPTH;
        }
        int currentDepth = 0;

        var possibleActions = GetAllViableActions(unit);

        Stack<TurnAction> currentActionSequence = new Stack<TurnAction>();
        TurnAction currentInitialMove = null;
        var bestAction = possibleActions[0];
        var bestDiscontentmentValue = float.MaxValue;

        bool victoryActionPlanFound = false;
        int leastStepsToWin = Int32.MaxValue;
        
        var DEBUG_loops = 0;
        var DEBUG_timestampStart = Time.realtimeSinceStartup;
        do
        {
            DEBUG_loops++;

            var isVictoryPlan = currentWorldModel.DidTeamWin(unit.UnitIdentifier.TeamId);
            var isLostPlan = currentWorldModel.DidTeamLose(unit.UnitIdentifier.TeamId);
            
            if (WorldModelStack.Count >= AIDepth ||  isVictoryPlan || isLostPlan)
            {
                var currentDiscontentment = currentWorldModel.GetDiscontentmentForUnit(unit);
                
                // Basically after finding a VictoryPlan stop considering those plans that don't end in likely victory
                if (!isVictoryPlan && victoryActionPlanFound)
                {
                    continue;
                }
                else if (!isVictoryPlan && !victoryActionPlanFound)
                {
                    if (currentDiscontentment < bestDiscontentmentValue)
                    {
                        bestAction = currentInitialMove;
                        bestDiscontentmentValue = currentDiscontentment;
                    }
                }
                else if (isVictoryPlan && !victoryActionPlanFound)
                {
                    victoryActionPlanFound = true;
                    bestDiscontentmentValue = currentDiscontentment;
                    leastStepsToWin = currentActionSequence.Count;

                }
                else if (isVictoryPlan && victoryActionPlanFound)
                {
                    if (currentDiscontentment < bestDiscontentmentValue)
                    {
                        if (leastStepsToWin > currentActionSequence.Count)
                        {
                            leastStepsToWin = currentActionSequence.Count;
                            bestAction = currentInitialMove;
                            bestDiscontentmentValue = currentDiscontentment;
                        }
                    }
                }

                try
                {
                    currentActionSequence.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                WorldModelStack.Pop();
                currentWorldModel = WorldModelStack.Peek();
            }
            else
            {
                // We should assume AI opponent (player) will make optimal moves as well, we do this at minimum depth to avoid bottlenecks (only checking state right after action)
                if (UnitHelpers.GetRelativeOwner(currentWorldModel.CurrentlyActiveUnit.TeamId, unit.UnitIdentifier.TeamId) == UnitRelativeOwner.Opponent)
                {
                    if (!currentWorldModel.IsProcessed)
                    {
                        if (currentWorldModel.TryGetUnit(currentWorldModel.CurrentlyActiveUnit, out var independentUnit))
                        {
                            var nextAction = CalculateGOBAction(independentUnit, currentWorldModel);
                            currentActionSequence.Push(nextAction);

                            var nextQueueState = currentWorldModel.GetNextQueueState();
                            var nextWorldState = new WorldModel(currentWorldModel.CopyUnits(), nextQueueState.Queue, nextQueueState.CurrentlyActiveUnit);
                            WorldModelStack.Push(nextWorldState);
                            nextWorldState.ApplyTurnAction(nextAction);

                            currentWorldModel.IsProcessed = true;
                        }
                    }
                    else
                    {
                        if (currentActionSequence.Count > 0)
                        {
                            currentActionSequence.Pop();
                        }
                        WorldModelStack.Pop();
                        currentWorldModel = WorldModelStack.Count > 0 ? WorldModelStack.Peek() : null;
                    }
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
                        if (currentActionSequence.Count > 0)
                        {
                            currentActionSequence.Pop();
                        }
                        WorldModelStack.Pop();
                        currentWorldModel = WorldModelStack.Count > 0 ? WorldModelStack.Peek() : null;
                    }
                }
            }
        } while (WorldModelStack.Count > 1);
        
        Debug.Log($"AI Calculation Report: loops: {DEBUG_loops}, time: {Time.realtimeSinceStartup - DEBUG_timestampStart}, action taken: {bestAction.CommonCommandData.unitAbility.AbilityName}");

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

    public TurnAction CalculateGOBAction(Unit unit, WorldModel currentWorldModel)
    {
        var nextQueueState = currentWorldModel.GetNextQueueState();

        TurnAction bestAction = null;
        float bestDiscontentment = float.MaxValue;
        
        foreach (var viableAction in currentWorldModel.GetNextAction())
        {
            var nextWorldState = new WorldModel(currentWorldModel.CopyUnits(), nextQueueState.Queue, nextQueueState.CurrentlyActiveUnit);
            WorldModelStack.Push(nextWorldState);
            nextWorldState.ApplyTurnAction(viableAction);

            var discontentmentForUnit =  nextWorldState.GetDiscontentmentForUnit(unit);
            if (discontentmentForUnit < bestDiscontentment)
            {
                bestAction = viableAction;
                bestDiscontentment = discontentmentForUnit;
            }

            WorldModelStack.Pop();
        }
        

        return bestAction;
    }
}
