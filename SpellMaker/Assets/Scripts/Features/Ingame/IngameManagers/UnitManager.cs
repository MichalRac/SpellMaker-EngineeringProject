using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Transform unitRoot;
    [SerializeField] private BaseCharacterMaster baseCharacterMaster;
    [SerializeField] private SpawnpointFetcher spawnpointFetcher;
    [SerializeField] private UnitListSO UnitListSO;

    [SerializeField] private TurnManager turnManager;

    private Dictionary<UnitIdentifier, BaseCharacterMaster> ActiveCharacters;
    public BaseCharacterMaster GetActiveCharacter(UnitIdentifier unitIdentifier) => ActiveCharacters[unitIdentifier];
    public Dictionary<UnitIdentifier, BaseCharacterMaster> GetAllActiveCharacters() => ActiveCharacters;


    private void Awake()
    {
        ActiveCharacters = new Dictionary<UnitIdentifier, BaseCharacterMaster>();
    }

    public void SpawnUnit(UnitIdentifier unitIdentifier)
    {
        var character = Instantiate(baseCharacterMaster, unitRoot);
        
        var unitDataSO = UnitListSO.GetUnitDataSO(unitIdentifier.unitClass);
        var unit = UnitFactory.GetUnit(unitIdentifier, unitDataSO);
        
        character.Initialize(unit, unitDataSO.unitClassMaster);

        character.transform.position = unitIdentifier.owner == UnitOwner.Player 
            ? spawnpointFetcher.GetNextPlayerStartPosition() 
            : spawnpointFetcher.GetNextEnemyStartPosition();
    
        if(unitIdentifier.owner == UnitOwner.Opponent)
            character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (!ActiveCharacters.ContainsKey(unitIdentifier))
        {
            ActiveCharacters.Add(unitIdentifier, character);
        }
        else
        {
            Debug.LogError($"[UnitManager] Unit {unitIdentifier.ToString()} was already spawned!");
        }
    }

    public void UpdateStatus(out List<UnitIdentifier> unitsToRemove)
    {
        unitsToRemove = new List<UnitIdentifier>();
        foreach (var baseCharacterMaster in ActiveCharacters)
        {
            if(baseCharacterMaster.Value.Unit.unitData.hp <= 0)
            {
                unitsToRemove.Add(baseCharacterMaster.Key);
            }
        }
        foreach(var unitToRemove in unitsToRemove)
        {
            ActiveCharacters.Remove(unitToRemove);
        }
    }

    public bool HasAnyCharacterLeft(UnitOwner owner)
    {
        foreach (var baseCharacterMaster in ActiveCharacters)
        {
            if (baseCharacterMaster.Key.owner == owner && baseCharacterMaster.Value.Unit.unitData.hp > 0)
                return true;
        }
        return false;
    }

    // Used implictly
    public void SpawnUnitCheat(bool isPlayer)
    {
        var newUnitIdentifier = new UnitIdentifier(isPlayer ? UnitOwner.Player : UnitOwner.Opponent, ActiveCharacters.Count + 1, default);
        SpawnUnit(newUnitIdentifier);
        turnManager.AddToQueue(newUnitIdentifier);
    }
}
