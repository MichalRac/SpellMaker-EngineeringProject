using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSlotMaster : MonoBehaviour
{
    [SerializeField] CharacterSlotPresenter characterSlotPresenter;

    private int slotID;
    private UnitOwner unitOwner;
    private UnitClass unitClass;

    private UnityAction<UnitOwner, int> onCharacterRemoved;


    public void Setup(int slotID, UnitOwner owner, UnityAction<UnitOwner, int> onCharacterRemoved)
    {
        this.slotID = slotID;
        this.onCharacterRemoved = onCharacterRemoved;
        this.unitOwner = owner;
        this.unitClass = UnitClass.Swordsman;

        UpdateDescription();

        if (owner == UnitOwner.Opponent)
        {
            characterSlotPresenter.OverrideColorScheme();
        }
    }

    public void DecrementSlotId()
    {
        slotID--;
    }

    public void UpdateDescription()
    {
        characterSlotPresenter.UpdateDescription(unitClass);
    }
    
    public void NextClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        unitClass = (int)unitClass == enumLenght - 1 ? (UnitClass)0 : unitClass + 1;
        UpdateDescription();
    }

    public void PreviousClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        unitClass = (int)unitClass == 0 ? (UnitClass)enumLenght - 1 : unitClass - 1;
        UpdateDescription();
    }

    public void RemoveCharacter()
    {
        onCharacterRemoved?.Invoke(unitOwner, slotID);
    }

    public void SetRemoveButtonActive(bool value)
    {
        characterSlotPresenter.SetRemoveButtonActive(value);
    }
}
