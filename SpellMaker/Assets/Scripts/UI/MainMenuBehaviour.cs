using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject gameCreator;

    private void Awake()
    {
        DOTween.Init();
    }

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
