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
        foreach(var unitIdentifier in sceneArguments.PlayerCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        foreach (var unitIdentifier in sceneArguments.OpponentCharactersIdentifiers)
        {
            unitManager.SpawnUnit(unitIdentifier);
        }

        turnManager.PrepareQueue(unitManager.GetAllActiveCharacters());

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

        playerActionManager.BeginPlayerActionPhase(activeCharacter, () => 
        {
            activeCharacter.ActivateActionEffects();
            activeCharacter.RefreshLabel();
            BeginTurn();
        });
    }
}
