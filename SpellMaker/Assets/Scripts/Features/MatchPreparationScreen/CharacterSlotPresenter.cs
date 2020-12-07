using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotPresenter : MonoBehaviour
{
    [SerializeField] private GameObject activeState;

    [SerializeField] private List<SpriteOverrideController> spriteOverrideControllers;
    [SerializeField] private TextMeshProUGUI classTitle;
    [SerializeField] private TextMeshProUGUI classDescription;
    [SerializeField] private Button removeCharacterButton;

    public void OverrideColorScheme()
    {
        classTitle.color = new Color(1f, 0.35f, 0.2f);
        foreach(var spriteOverrideController in spriteOverrideControllers)
        {
            spriteOverrideController.UseOverride();
        }
    }

    public void UpdateDescription(UnitClass unitClass)
    {
        classTitle.text = unitClass.ToString();
        SetClassDescription(unitClass);
    }

    public void SetClassDescription(UnitClass unitClass)
    {
        string description = string.Empty;

        switch (unitClass)
        {
            case UnitClass.Swordsman:
                description = "High damage";
                break;
            case UnitClass.Knight:
                description = "High defense";
                break;
            case UnitClass.Priest:
                description = "Support";
                break;
            default:
                description = "#missing description";
                break;
        }

        classDescription.text = description;
    }

    public void SetRemoveButtonActive(bool value)
    {
        removeCharacterButton.gameObject.SetActive(value);
    }
}
