using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] Transform unitRoot;
    [SerializeField] BaseCharacterMaster baseCharacterMaster;
    [SerializeField] SpawnpointFetcher spawnpointFetcher;

    private Dictionary<UnitIdentifier, BaseCharacterMaster> ActiveCharacters;
    public BaseCharacterMaster GetActiveCharacter(UnitIdentifier unitIdentifier) => ActiveCharacters[unitIdentifier];
    public Dictionary<UnitIdentifier, BaseCharacterMaster> GetAllActiveCharacters() => ActiveCharacters;

    private void Awake()
    {
        ActiveCharacters = new Dictionary<UnitIdentifier, BaseCharacterMaster>();
    }

    public void SpawnUnit(UnitData data)
    {
        var character = Instantiate(baseCharacterMaster, unitRoot);
        character.Initialize(data);
        character.transform.position = data.unitIdentifier.owner == UnitOwner.Player 
            ? spawnpointFetcher.GetNextPlayerStartPosition() 
            : spawnpointFetcher.GetNextEnemyStartPosition();
    
        if(data.unitIdentifier.owner == UnitOwner.Opponent)
            character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (!ActiveCharacters.ContainsKey(data.unitIdentifier))
        {
            ActiveCharacters.Add(data.unitIdentifier, character);
        }
        else
        {
            Debug.LogError($"[UnitManager] Unit {data.unitIdentifier.ToString()} was already spawned!");
        }
    }
}
