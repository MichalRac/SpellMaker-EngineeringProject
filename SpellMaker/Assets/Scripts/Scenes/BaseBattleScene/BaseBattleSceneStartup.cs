using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SceneStartup("BaseBattleScene")]
public class BaseBattleSceneStartup : SceneStartup<BaseBattleSceneStartup, BaseBattleSceneArgs>
{
    [SerializeField] MasterManager masterManager;

    protected override void OnAwake()
    {
        //SceneStartupManager.UnloadLoadingScene();
        //SceneManager.SetActiveScene(SceneManager.GetActiveScene());

        StartCoroutine(masterManager.Initialize(Args));   
    }
}
