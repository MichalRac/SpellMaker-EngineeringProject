using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPreparationMaster : MonoBehaviour
{
    private const int MAX_CHARACTERS_PER_TEAM = 3;

    [SerializeField] private MatchPreparationPresenter matchPreparationPresenter;
    [SerializeField] private CharacterSlotMaster characterSlotMaster;

    private Dictionary<UnitOwner, List<CharacterSlotMaster>> matchupData;

    private void Awake()
    {
        matchupData = new Dictionary<UnitOwner, List<CharacterSlotMaster>>();
        matchupData.Add(UnitOwner.Player, new List<CharacterSlotMaster>());
        matchupData.Add(UnitOwner.Opponent, new List<CharacterSlotMaster>());
    }

    private void OnEnable()
    {
        matchPreparationPresenter = Instantiate(matchPreparationPresenter, transform);
        matchPreparationPresenter.Setup(OnMatchupConfirmed, () => AddCharacter(UnitOwner.Player), () => AddCharacter(UnitOwner.Opponent));
        PrepareInitialCharacters();
    }

    private void OnDisable()
    {
        matchPreparationPresenter.CleanupCharacterSlots();
    }

    private void PrepareInitialCharacters()
    {
        AddCharacter(UnitOwner.Player, true);
        AddCharacter(UnitOwner.Opponent, true);
    }

    private void AddCharacter(UnitOwner owner, bool initialCharacter = false)
    {
        matchupData[owner].Add(matchPreparationPresenter.CreateSlot(matchupData[owner].Count, characterSlotMaster, owner, OnAnyCharacterRemoved));
        SetRemovableStates(owner);
        SetCharacterAddableState(owner);
    }

    private void OnMatchupConfirmed()
    {
        List<UnitIdentifier> playerCharacters = new List<UnitIdentifier>();
        List<UnitIdentifier> opponentCharacters = new List<UnitIdentifier>();

        foreach (var slot in matchupData[UnitOwner.Player])
            playerCharacters.Add(slot.UnitIdentifier);
        foreach (var slot in matchupData[UnitOwner.Opponent])
            opponentCharacters.Add(slot.UnitIdentifier);

        SceneStartupManager.OpenSceneWithArgs<BaseBattleSceneStartup, BaseBattleSceneArgs>
            (BaseBattleSceneBuilder.GetBaseBattleSceneArgs(playerCharacters, opponentCharacters));
    }

    private void OnAnyCharacterRemoved(UnitOwner owner, int slotId)
    {
        matchupData[owner].RemoveAt(slotId);

        for(int i = slotId; i < matchupData[owner].Count; i++)
        {
            matchupData[owner][i].UnitIdentifier.uniqueId--;
        }

        SetRemovableStates(owner);
        SetCharacterAddableState(owner);    
    }

    private void SetRemovableStates(UnitOwner owner)
    {
        var canRemovePlayerCharacters = matchupData[owner].Count > 1;
        foreach (var character in matchupData[owner])
            character.SetRemoveButtonActive(canRemovePlayerCharacters);
    }

    private void SetCharacterAddableState(UnitOwner owner)
    {
        matchPreparationPresenter.SetAddCharacterButton(owner, matchupData[owner].Count < MAX_CHARACTERS_PER_TEAM);
    }
}
