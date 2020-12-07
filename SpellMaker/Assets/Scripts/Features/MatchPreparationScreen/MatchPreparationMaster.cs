using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPreparationMaster : MonoBehaviour
{
    private const int MAX_CHARACTERS_PER_TEAM = 3;

    [SerializeField] private MatchPreparationPresenter matchPreparationPresenter;
    [SerializeField] private CharacterSlotMaster characterSlotMaster;
    private List<CharacterSlotMaster> playerCharacters;
    private List<CharacterSlotMaster> opponentCharacters;

    private void Awake()
    {
        playerCharacters = new List<CharacterSlotMaster>();
        opponentCharacters = new List<CharacterSlotMaster>();
    }

    private void OnEnable()
    {
        matchPreparationPresenter = Instantiate(matchPreparationPresenter, transform);
        matchPreparationPresenter.Setup(OnMatchupConfirmed);
        PrepareInitialCharacters();
    }

    private void OnDisable()
    {
        matchPreparationPresenter.CleanupCharacterSlots();
    }

    private void PrepareInitialCharacters()
    {

        for(int i = 0; i < MAX_CHARACTERS_PER_TEAM; i++)
        {
            playerCharacters.Add( matchPreparationPresenter.CreateSlot(characterSlotMaster, UnitOwner.Player, i == 0) );
            opponentCharacters.Add( matchPreparationPresenter.CreateSlot(characterSlotMaster, UnitOwner.Opponent, i == 0) );
        }
    }

    private void OnMatchupConfirmed()
    {
        var baseBattleSceneArgs = new BaseBattleSceneArgs();
        baseBattleSceneArgs.PlayerCharacters = playerCharacters.FindAll(character => character.IsActive).Count;
        baseBattleSceneArgs.OpponentCharacters = opponentCharacters.FindAll(character => character.IsActive).Count;

        SceneStartupManager.OpenSceneWithArgs<BaseBattleSceneStartup, BaseBattleSceneArgs>(baseBattleSceneArgs);
    }
}
