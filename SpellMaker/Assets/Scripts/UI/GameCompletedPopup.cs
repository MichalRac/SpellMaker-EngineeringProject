using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCompletedPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameCompletedPopupLabel;
    
    public void Show(bool isVictory)
    {
        gameObject.SetActive(true);
        gameCompletedPopupLabel.text = isVictory ? "You won!" : "You lost!";
    }

    public void OnButtonClicked()
    {
        SceneStartupManager.OpenSceneWithArgs<MainMenuStartup, MainMenuArgs>(new MainMenuArgs());
    }
}
