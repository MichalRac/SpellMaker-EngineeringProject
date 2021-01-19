using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilityManager : MonoBehaviour
{
    [SerializeField] List<AbilitySetupSO> unitAbilities;
    public List<AbilitySetupSO> GetUnitAbilitySetups() => unitAbilities; 
}
