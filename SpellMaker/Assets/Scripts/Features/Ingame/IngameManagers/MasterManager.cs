using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] UnitManager unitManager;
    [SerializeField] BattleSceneManager battleSceneManager;
    [SerializeField] PlayerActionManager playerActionManager;

    public void Initialize(BaseBattleSceneArgs sceneArguments)
    {
        new WorldModelService();
        
        foreach(var unitIdentifier in sceneArguments.PlayerCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        foreach (var unitIdentifier in sceneArguments.OpponentCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        turnManager.PrepareQueue(unitManager.GetAllActiveCharacters());
        
        WorldModelService.Instance.Initialize( unitManager.PrepareUnitCopiesForWorldModel(), turnManager.queue, turnManager.active );

        BeginTurn();
    }



    public void BeginTurn()
    {
        unitManager.UpdateStatus(out var removedUnits);
        turnManager.UpdateStatus(removedUnits);
        
        if(!unitManager.HasAnyCharacterLeft(0) || !unitManager.HasAnyCharacterLeft(1))
        {
            // Handle end game flow
            Debug.Log("Game completed!");
            return;
        }

        var nextInQueue = turnManager.GetNextInQueue();
        var activeCharacter = unitManager.GetActiveCharacter(nextInQueue);
        
        /*foreach (var unitAbility in activeCharacter.Unit.UnitData.UnitAbilities)
        {
            var possibleAbilityUses = unitAbility.GetPossibleAbilityUses(activeCharacter.Unit, WorldModelService.Instance.GetCurrentWorldModelLayer());

            foreach (var possibleAbilityUse in possibleAbilityUses)
            {
                if (possibleAbilityUse.commonCommandData.targetsIdentifiers.Count > 0)
                {
                    Debug.Log(
                        $"Can use ability {unitAbility.AbilityName} at {possibleAbilityUse.optionalCommandData.Position} on {possibleAbilityUse.commonCommandData.targetsIdentifiers[0].UniqueId} with {possibleAbilityUse.commonCommandData.targetsIdentifiers.Count} targets");
                }
                else
                {
                    Debug.Log(
                        $"An ability {unitAbility.AbilityName} with {possibleAbilityUse.commonCommandData.targetsIdentifiers.Count} hits has been found as viable");
                }
            }
        }*/

        if (activeCharacter.Unit.UnitIdentifier.TeamId == 0)
        {
            playerActionManager.BeginPlayerActionPhase(activeCharacter, () => 
            {
                activeCharacter.ActivateActionEffects();
                activeCharacter.RefreshLabel();
                BeginTurn();
            });

        }
        else
        {
            WorldModelService.Instance.Initialize( unitManager.PrepareUnitCopiesForWorldModel(), turnManager.queue, nextInQueue);
            new AbilitySequenceHandler(WorldModelService.Instance.CalculateAIAction(activeCharacter.Unit), () =>
            {
                activeCharacter.ActivateActionEffects();
                activeCharacter.RefreshLabel();
                BeginTurn();
            }).Begin();

        }
    }
}
