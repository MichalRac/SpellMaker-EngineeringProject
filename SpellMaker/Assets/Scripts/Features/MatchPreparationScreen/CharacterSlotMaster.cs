using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSlotMaster : MonoBehaviour
{
    [SerializeField] CharacterSlotPresenter characterSlotPresenter;

    public int SlotID { get; private set; }
    public UnitRelativeOwner UnitOwner { get; private set; }
    public UnitClass UnitClass { get; private set; }
    public UnitIdentifier UnitIdentifier { get; private set; }

    private UnityAction<UnitRelativeOwner, int> onCharacterRemoved;

    public void Setup(int slotID, UnitRelativeOwner owner, UnityAction<UnitRelativeOwner, int> onCharacterRemoved)
    {
        this.SlotID = slotID;
        this.onCharacterRemoved = onCharacterRemoved;
        this.UnitOwner = owner;
        this.UnitClass = default;

        this.UnitIdentifier = new UnitIdentifier(owner == UnitRelativeOwner.Self ? 0 : 1, slotID, default);

        UpdateDescription();

        if (owner == UnitRelativeOwner.Opponent)
        {
            characterSlotPresenter.OverrideColorScheme();
        }
    }

    public void DecrementSlotId()
    {
        SlotID--;
    }

    public void UpdateDescription()
    {
        characterSlotPresenter.UpdateDescription(UnitClass);
    }
    
    // used implicitly
    public void NextClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        UnitClass = (int)UnitClass == enumLenght - 1 ? (UnitClass)0 : UnitClass + 1;
        UnitIdentifier.SwitchClass(UnitClass);
        UpdateDescription();
    }

    // used implicitly
    public void PreviousClass()
    {
        var enumLenght = System.Enum.GetNames(typeof(UnitClass)).Length;
        UnitClass = (int)UnitClass == 0 ? (UnitClass)enumLenght - 1 : UnitClass - 1;
        UnitIdentifier.SwitchClass(UnitClass);
        UpdateDescription();
    }

    public void RemoveCharacter()
    {
        onCharacterRemoved?.Invoke(UnitOwner, SlotID);
    }

    public void SetRemoveButtonActive(bool value)
    {
        characterSlotPresenter.SetRemoveButtonActive(value);
    }
}
