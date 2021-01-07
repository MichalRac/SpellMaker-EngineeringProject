using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSlotMaster : MonoBehaviour
{
    [SerializeField] CharacterSlotPresenter characterSlotPresenter;

    private int slotID;

    public UnitOwner UnitOwner { get; private set; }
    public UnitClass UnitClass { get; private set; }
    public UnitData UnitData { get; private set; }

    private UnityAction<UnitOwner, int> onCharacterRemoved;

    public void Setup(int slotID, UnitOwner owner, UnityAction<UnitOwner, int> onCharacterRemoved)
    {
        this.slotID = slotID;
        this.onCharacterRemoved = onCharacterRemoved;
        this.UnitOwner = owner;
        this.UnitClass = default;

        UnitData = new UnitData(30, 0, owner == UnitOwner.Player ? Color.green : Color.red, default);

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
        characterSlotPresenter.UpdateDescription(UnitClass);
    }
    
    public void NextClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        UnitClass = (int)UnitClass == enumLenght - 1 ? (UnitClass)0 : UnitClass + 1;
        UnitData.unitClass = UnitClass;
        UpdateDescription();
    }

    public void PreviousClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        UnitClass = (int)UnitClass == 0 ? (UnitClass)enumLenght - 1 : UnitClass - 1;
        UnitData.unitClass = UnitClass;
        UpdateDescription();
    }

    public void RemoveCharacter()
    {
        onCharacterRemoved?.Invoke(UnitOwner, slotID);
    }

    public void SetRemoveButtonActive(bool value)
    {
        characterSlotPresenter.SetRemoveButtonActive(value);
    }
}
