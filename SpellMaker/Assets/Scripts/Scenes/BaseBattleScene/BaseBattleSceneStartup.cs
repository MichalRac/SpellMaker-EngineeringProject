using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SceneStartup("BaseBattleScene")]
public class BaseBattleSceneStartup : SceneStartup<BaseBattleSceneStartup, BaseBattleSceneArgs>
{
    [SerializeField] MasterManager masterManager;

    protected override void OnAwake()
    {
        // When BaseBattleScene is loaded as first scene add one character for each team as a shortcut
        if (Args.OpponentCharactersIdentifiers == null || Args.PlayerCharactersIdentifiers == null)
        {
            masterManager.Initialize(BaseBattleSceneBuilder.GetBaseBattleSceneArgs(
                new List<UnitIdentifier>() { new UnitIdentifier(UnitOwner.Player, 0, default) }, 
                new List<UnitIdentifier>() { new UnitIdentifier(UnitOwner.Opponent, 1, default) }));
        }
        else
        {
            masterManager.Initialize(Args);
        }
    }
}
