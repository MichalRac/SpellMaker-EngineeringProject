using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject gameCreator;

    public void OnPlayButtonClicked()
    {
        gameCreator.gameObject.SetActive(true);
        var Args = new BaseBattleSceneArgs { Enemies = 3 };
        SceneStartupManager.OpenSceneWithArgs<BaseBattleSceneStartup, BaseBattleSceneArgs>();
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public static void OpenScene()
    {
       
    }
}
