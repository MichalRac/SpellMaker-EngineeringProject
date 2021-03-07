using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitAnimationController))]
[RequireComponent(typeof(UnitAbilityManager))]
public class UnitClassMaster : MonoBehaviour
{
    private static readonly int PLAYER_TEAM_ID = 0;
    private Color defaultColor;
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;

    public UnitAbilityManager UnitAbilityManager { get; private set; }
    public UnitAnimationController UnitAnimationController { get; private set; }

    public void Awake()
    {
        UnitAbilityManager = GetComponent<UnitAbilityManager>();
        UnitAnimationController = GetComponent<UnitAnimationController>();
    }

    public void SetTeamColor(int teamId)
    {
        defaultColor = teamId == PLAYER_TEAM_ID ? Color.green : Color.red;
        characterMeshRenderer.material.color = defaultColor;
    }

    public void ToggleShieldColor(bool value)
    {
        characterMeshRenderer.material.color = value ? Color.blue : defaultColor;
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
