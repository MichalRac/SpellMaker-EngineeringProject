using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] Transform unitRoot;
    [SerializeField] BaseCharacterMaster baseCharacterMaster;

    // Manage Units
    public void SpawnUnit(UnitData data)
    {
        Instantiate(baseCharacterMaster, unitRoot).Initialize(data);
    }
}
