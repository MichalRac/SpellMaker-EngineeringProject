using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BaseBattleSceneArgs : SceneArgs
{
    public List<UnitIdentifier> PlayerCharactersIdentifiers { get; set; }
    public List<UnitIdentifier> OpponentCharactersIdentifiers { get; set; }
}

public static class BaseBattleSceneBuilder
{
    public static BaseBattleSceneArgs GetBaseBattleSceneArgs(List<UnitIdentifier> PlayerCharacters, List<UnitIdentifier> OpponentCharacters)
    {
        var baseBattleSceneArgs = new BaseBattleSceneArgs();

        List<UnitIdentifier> playerCharacters = PlayerCharacters;
        List<UnitIdentifier> opponentCharacters = OpponentCharacters;

        baseBattleSceneArgs.PlayerCharactersIdentifiers = playerCharacters;
        baseBattleSceneArgs.OpponentCharactersIdentifiers = opponentCharacters;

        return baseBattleSceneArgs;
    }
}