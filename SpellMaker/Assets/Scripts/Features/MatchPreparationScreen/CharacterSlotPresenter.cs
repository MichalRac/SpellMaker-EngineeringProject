using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterSlotPresenter : MonoBehaviour
{
    [SerializeField] private GameObject activeState;
    [SerializeField] private GameObject inactiveState;

    [SerializeField] private List<SpriteOverrideController> spriteOverrideControllers;
    [SerializeField] private TextMeshProUGUI classTitle;

    public void SetActiveState(bool value)
    {
        activeState.SetActive(value);
        inactiveState.SetActive(!value);
    }

    public void OverrideColorScheme()
    {
        classTitle.color = new Color(1f, 0.35f, 0.2f);
        foreach(var spriteOverrideController in spriteOverrideControllers)
        {
            spriteOverrideController.UseOverride();
        }
    }
}
