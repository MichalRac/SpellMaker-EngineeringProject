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
        
        WorldModelService.Instance.Initialize( unitManager.PrepareUnitCopiesForWorldModel() );

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

        
        WorldModelService.Instance.Initialize( unitManager.PrepareUnitCopiesForWorldModel() );
        foreach (var unitAbility in activeCharacter.Unit.UnitData.UnitAbilities)
        {
            var possibleAbilityUses = unitAbility.GetPossibleAbilityUses(activeCharacter.Unit, WorldModelService.Instance.GetCurrentWorldModelLayer());

            foreach (var possibleAbilityUse in possibleAbilityUses)
            {
                Debug.Log( $"Can use ability {unitAbility.AbilityName} on {possibleAbilityUse.commonCommandData.targetsIdentifiers[0].UniqueId} with {possibleAbilityUse.commonCommandData.targetsIdentifiers.Count} hits");
            }
        }

        playerActionManager.BeginPlayerActionPhase(activeCharacter, () => 
        {
            activeCharacter.ActivateActionEffects();
            activeCharacter.RefreshLabel();
            BeginTurn();
        });
    }
}
