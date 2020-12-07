using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] UnitManager unitManager;
    [SerializeField] BattleSceneManager battleSceneManager;

    public void Initialize(BaseBattleSceneArgs sceneArguments)
    {
        for(int i = 0; i < sceneArguments.PlayerCharacters; i++)
        {
            unitManager.SpawnUnit(new UnitData { characterId = 0, color = Color.green, hp = 30 });
        }

        for (int i = 0; i < sceneArguments.OpponentCharacters; i++)
        {
            unitManager.SpawnUnit(new UnitData { characterId = 0, color = Color.red, hp = 30 });
        }
    }
}
