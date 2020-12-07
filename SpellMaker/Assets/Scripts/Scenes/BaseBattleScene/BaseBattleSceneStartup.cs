using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SceneStartup("BaseBattleScene")]
public class BaseBattleSceneStartup : SceneStartup<BaseBattleSceneStartup, BaseBattleSceneArgs>
{
    protected override void OnAwake()
    {
        Debug.Log($"Populating scene with {Args.OpponentCharacters} enemies");
    }
}
