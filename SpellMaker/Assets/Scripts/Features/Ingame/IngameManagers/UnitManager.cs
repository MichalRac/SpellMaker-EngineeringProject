using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] Transform unitRoot;
    [SerializeField] BaseCharacterMaster baseCharacterMaster;
    [SerializeField] SpawnpointFetcher spawnpointFetcher;

    private Dictionary<UnitOwner, List<BaseCharacterMaster>> ActiveCharacters;
    public BaseCharacterMaster GetActiveCharacter(UnitIdentifier unitIdentifier) => ActiveCharacters[unitIdentifier.owner][unitIdentifier.uniqueId];

    private void Awake()
    {
        ActiveCharacters = new Dictionary<UnitOwner, List<BaseCharacterMaster>>();
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

        if (!ActiveCharacters.ContainsKey(data.unitIdentifier.owner))
            ActiveCharacters.Add(data.unitIdentifier.owner, new List<BaseCharacterMaster>());

        ActiveCharacters[data.unitIdentifier.owner].Add(character);
    }
}
