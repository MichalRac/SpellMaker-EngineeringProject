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
        var unitID = 0;

        foreach(var unit in sceneArguments.PlayerCharacters)
        {
            unitManager.SpawnUnit(UnitFactory.GetUnit(unit.unitIdentifier.owner, unitID++, unit.unitData));
        }

        foreach (var unit in sceneArguments.OpponentCharacters)
        {
            unitManager.SpawnUnit(UnitFactory.GetUnit(unit.unitIdentifier.owner, unitID++, unit.unitData));
        }

        turnManager.PrepareQueue(unitManager.GetAllActiveCharacters());

        BeginTurn();
    }



    public void BeginTurn()
    {
        unitManager.UpdateStatus(out var removedUnits);
        turnManager.UpdateStatus(removedUnits);

        if(!unitManager.HasAnyCharacterLeft(UnitOwner.Player) || !unitManager.HasAnyCharacterLeft(UnitOwner.Opponent))
        {
            // Handle end game flow
            Debug.Log("Game completed!");
        }

        var nextInQueue = turnManager.GetNextInQueue();
        var activeCharacter = unitManager.GetActiveCharacter(nextInQueue);

        playerActionManager.BeginPlayerActionPhase(activeCharacter, BeginTurn);
    }
}
