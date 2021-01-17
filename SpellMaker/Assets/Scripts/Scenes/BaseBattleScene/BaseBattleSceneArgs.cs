using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BaseBattleSceneArgs : SceneArgs
{
    public List<Unit> PlayerCharacters { get; set; }
    public List<Unit> OpponentCharacters { get; set; }
}

public static class BaseBattleSceneBuilder
{
    public static BaseBattleSceneArgs GetBaseBattleSceneArgs(List<Unit> PlayerCharacters, List<Unit> OpponentCharacters)
    {
        var baseBattleSceneArgs = new BaseBattleSceneArgs();

        List<Unit> playerCharacters = PlayerCharacters;
        List<Unit> opponentCharacters = OpponentCharacters;

        baseBattleSceneArgs.PlayerCharacters = playerCharacters;
        baseBattleSceneArgs.OpponentCharacters = opponentCharacters;

        return baseBattleSceneArgs;
    }
}