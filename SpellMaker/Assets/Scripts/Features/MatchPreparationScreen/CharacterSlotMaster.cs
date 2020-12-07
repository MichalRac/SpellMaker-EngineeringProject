using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotMaster : MonoBehaviour
{
    [SerializeField] CharacterSlotPresenter characterSlotPresenter;
    public bool IsActive { get; private set; } 

    public void Setup(UnitOwner owner, bool isInitialSlot = false)
    {
        characterSlotPresenter.SetActiveState(isInitialSlot);
        IsActive = isInitialSlot;
    
        if(owner == UnitOwner.Opponent)
        {
            characterSlotPresenter.OverrideColorScheme();
        }
    }
}
