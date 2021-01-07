using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BaseBattleSceneArgs : SceneArgs
{
    public List<CharacterSlotMaster> PlayerCharacters { get; set; }
    public List<CharacterSlotMaster> OpponentCharacters { get; set; }
}
