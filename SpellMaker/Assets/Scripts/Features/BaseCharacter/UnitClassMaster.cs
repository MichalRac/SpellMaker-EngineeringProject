using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitAnimationController))]
[RequireComponent(typeof(UnitAbilityManager))]
public class UnitClassMaster : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;

    public UnitAbilityManager UnitAbilityManager { get; private set; }
    public UnitAnimationController UnitAnimationController { get; private set; }

    public void Awake()
    {
        UnitAbilityManager = GetComponent<UnitAbilityManager>();
        UnitAnimationController = GetComponent<UnitAnimationController>();
    }

    public void SetTeamColor(UnitOwner owner)
    {
        characterMeshRenderer.material.color = owner == UnitOwner.Player ? Color.green : Color.red;
    }


    public List<UnitAbility> GetUnitAbilities()
    {
        var unitAbilities = new List<UnitAbility>();

        // In the future each ability will be determined here if it's active or not
        foreach(var abilitySetup in UnitAbilityManager.GetUnitAbilitySetups())
        {
            unitAbilities.Add(UnitAbilityFactory.GetUnitAbility(abilitySetup));
        }

        return unitAbilities;
    }

}
