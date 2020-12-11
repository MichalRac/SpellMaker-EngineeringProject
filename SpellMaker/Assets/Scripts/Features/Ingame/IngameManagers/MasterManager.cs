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
        if (sceneArguments.IsNull)
            sceneArguments = new BaseBattleSceneArgs() { OpponentCharacters = 3, PlayerCharacters = 3, IsNull = false };

        var unitID = 0;

        for (int i = 0; i < sceneArguments.PlayerCharacters; i++)
            unitManager.SpawnUnit(new UnitData { unitIdentifier = new UnitIdentifier(UnitOwner.Player, unitID++), characterId = 0, color = Color.green, hp = 30 });

        for (int i = 0; i < sceneArguments.OpponentCharacters; i++)
            unitManager.SpawnUnit(new UnitData { unitIdentifier = new UnitIdentifier(UnitOwner.Opponent, unitID++), characterId = 0, color = Color.red, hp = 30 });

        turnManager.PrepareQueue(unitManager.GetAllActiveCharacters());

        BeginNextTurn();
    }



    public void BeginNextTurn()
    {
        if(!unitManager.HasAnyCharacterLeft(UnitOwner.Player) || !unitManager.HasAnyCharacterLeft(UnitOwner.Opponent))
        {
            // Handle end game flow
            Debug.Log("Game completed!");
        }

        var nextInQueue = turnManager.GetNextInQueue();

        playerActionManager.BeginPlayerActionPhase(unitManager.GetActiveCharacter(nextInQueue), BeginNextTurn);
    }
}
