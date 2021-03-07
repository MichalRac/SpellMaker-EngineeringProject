using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPreparationMaster : MonoBehaviour
{
    private const int MAX_CHARACTERS_PER_TEAM = 3;

    [SerializeField] private MatchPreparationPresenter matchPreparationPresenter;
    [SerializeField] private CharacterSlotMaster characterSlotMaster;

    private Dictionary<UnitRelativeOwner, List<CharacterSlotMaster>> matchupData;
    private int uniqueId;

    private void Awake()
    {
        matchupData = new Dictionary<UnitRelativeOwner, List<CharacterSlotMaster>>();
        matchupData.Add(UnitRelativeOwner.Self, new List<CharacterSlotMaster>());
        matchupData.Add(UnitRelativeOwner.Opponent, new List<CharacterSlotMaster>());

        uniqueId = 0;
    }

    private void OnEnable()
    {
        matchPreparationPresenter = Instantiate(matchPreparationPresenter, transform);
        matchPreparationPresenter.Setup(OnMatchupConfirmed, () => AddCharacter(UnitRelativeOwner.Self), () => AddCharacter(UnitRelativeOwner.Opponent));
        PrepareInitialCharacters();
    }

    private void OnDisable()
    {
        matchPreparationPresenter.CleanupCharacterSlots();
    }

    private void PrepareInitialCharacters()
    {
        AddCharacter(UnitRelativeOwner.Self, true);
        AddCharacter(UnitRelativeOwner.Opponent, true);
    }

    private void AddCharacter(UnitRelativeOwner owner, bool initialCharacter = false)
    {
        matchupData[owner].Add(matchPreparationPresenter.CreateSlot(uniqueId++, characterSlotMaster, owner, OnAnyCharacterRemoved));
        SetRemovableStates(owner);
        SetCharacterAddableState(owner);
    }

    private void OnMatchupConfirmed()
    {
        List<UnitIdentifier> playerCharacters = new List<UnitIdentifier>();
        List<UnitIdentifier> opponentCharacters = new List<UnitIdentifier>();

        foreach (var slot in matchupData[UnitRelativeOwner.Self])
            playerCharacters.Add(slot.UnitIdentifier);
        foreach (var slot in matchupData[UnitRelativeOwner.Opponent])
            opponentCharacters.Add(slot.UnitIdentifier);

        SceneStartupManager.OpenSceneWithArgs<BaseBattleSceneStartup, BaseBattleSceneArgs>
            (BaseBattleSceneBuilder.GetBaseBattleSceneArgs(playerCharacters, opponentCharacters));
    }

    private void OnAnyCharacterRemoved(UnitRelativeOwner owner, int slotId)
    {
        var slotToRemove = matchupData[owner].Find((slot) => slot.SlotID == slotId);
        
        if(slotToRemove != null)
        {
            matchupData[owner].Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);

            SetRemovableStates(owner);
            SetCharacterAddableState(owner);
        }
        else
        {
            Debug.LogError($"[MatchPreparationMaster] Trying to remove unassigned slot id {slotId}");
        }
    }

    private void SetRemovableStates(UnitRelativeOwner owner)
    {
        var canRemovePlayerCharacters = matchupData[owner].Count > 1;
        foreach (var character in matchupData[owner])
            character.SetRemoveButtonActive(canRemovePlayerCharacters);
    }

    private void SetCharacterAddableState(UnitRelativeOwner owner)
    {
        matchPreparationPresenter.SetAddCharacterButton(owner, matchupData[owner].Count < MAX_CHARACTERS_PER_TEAM);
    }
}
