using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPreparationMaster : MonoBehaviour
{
    private const int MAX_CHARACTERS_PER_TEAM = 3;

    [SerializeField] private MatchPreparationPresenter matchPreparationPresenter;
    [SerializeField] private CharacterSlotMaster characterSlotMaster;
/*    private List<CharacterSlotMaster> playerCharacters;
    private List<CharacterSlotMaster> opponentCharacters;
*/
    private Dictionary<UnitOwner, List<CharacterSlotMaster>> matchupData;

    private void Awake()
    {
        matchupData = new Dictionary<UnitOwner, List<CharacterSlotMaster>>();
        matchupData.Add(UnitOwner.Player, new List<CharacterSlotMaster>());
        matchupData.Add(UnitOwner.Opponent, new List<CharacterSlotMaster>());

/*        playerCharacters = new List<CharacterSlotMaster>();
        opponentCharacters = new List<CharacterSlotMaster>();
*/    }

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
        var baseBattleSceneArgs = new BaseBattleSceneArgs();
        baseBattleSceneArgs.PlayerCharacters = matchupData[UnitOwner.Player].Count;
        baseBattleSceneArgs.OpponentCharacters = matchupData[UnitOwner.Opponent].Count;

        SceneStartupManager.OpenSceneWithArgs<BaseBattleSceneStartup, BaseBattleSceneArgs>(baseBattleSceneArgs);
    }

    private void OnAnyCharacterRemoved(UnitOwner owner, int slotId)
    {
        Destroy(matchupData[owner][slotId].gameObject);
        matchupData[owner].RemoveAt(slotId);

        for(int i = slotId; i < matchupData[owner].Count; i++)
        {
            matchupData[owner][i].DecrementSlotId();
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
