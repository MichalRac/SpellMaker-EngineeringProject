using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SceneStartup("BaseBattleScene")]
public class BaseBattleSceneStartup : SceneStartup<BaseBattleSceneStartup, BaseBattleSceneArgs>
{
    [SerializeField] MasterManager masterManager;

    protected override void OnAwake()
    {
        masterManager.Initialize(Args);   
    }
}
