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
        gameObject.SetActive(false);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public static void OpenScene()
    {
       
    }
}
