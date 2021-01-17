using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SceneStartup("BaseBattleScene")]
public class BaseBattleSceneStartup : SceneStartup<BaseBattleSceneStartup, BaseBattleSceneArgs>
{
    [SerializeField] MasterManager masterManager;

    protected override void OnAwake()
    {
        // When BaseBattleScene is loaded first add one character for each team
        if (Args.OpponentCharacters == null || Args.PlayerCharacters == null)
        {
            masterManager.Initialize(BaseBattleSceneBuilder.GetBaseBattleSceneArgs(
                new List<Unit>() { UnitFactory.GetBasicUnit(UnitOwner.Player, 0) }, 
                new List<Unit>() { UnitFactory.GetBasicUnit(UnitOwner.Opponent, 1) }));
        }
        else
        {
            masterManager.Initialize(Args);
        }
    }
}
