using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    public enum ActionMode
    {
        Disabled = 0,
        HUD = 1,
        Targeting = 2,
        PickAbility = 3,
    }

    BaseCharacterMaster activeCharacter;
    UnitAbility activeAbility;

    Action onActionPhaseCompleted;

    [SerializeField] private ActionSelection ActionSelection;
    [SerializeField] private TargeterMaster Targeting;
    [SerializeField] private GameObject TauntInfo;

    public void SetActionMode(ActionMode actionMode, UnitAbility ability = null)
    {
        activeAbility = ability;

        Targeting.CancelTargeting();

        switch (actionMode)
        {
            case ActionMode.Disabled:
                Targeting.gameObject.SetActive(false);
                ActionSelection.gameObject.SetActive(false);
                break;
            case ActionMode.HUD:
                Targeting.gameObject.SetActive(false);
                ActionSelection.gameObject.SetActive(true);
                ActionSelection.Setup(GetBasicActions());
                break;
            case ActionMode.Targeting:
                Targeting.gameObject.SetActive(true);
                Targeting.StartTargeting(activeCharacter.Unit.UnitIdentifier.TeamId, activeCharacter.transform.position, ability, OnTargetsChosen);
                ActionSelection.Setup(GetTargetingActions());
                break;
            case ActionMode.PickAbility:
                Targeting.gameObject.SetActive(false);
                ActionSelection.gameObject.SetActive(true);
                ActionSelection.Setup(GetAbilityActions());
                break;
        }
    }

    public void OnTargetsChosen(TargetingResultData targetingResultData)
    {
        if(activeAbility == null)
        {
            Debug.LogError("[PlayerActionManager] Trying to dispatch OnTargetsChosen with no active ability!");
            return;
        }

        Debug.Log($"Dispatching ability {activeAbility.AbilityName} on {targetingResultData.targetPoint} with {targetingResultData.unitIdentifiers.Count}");

        var commonCommandData = new CommonCommandData(activeCharacter.GetUnitIdentifier(), targetingResultData.unitIdentifiers, activeAbility, null);
        var optionalCommandData = new OptionalCommandData(targetingResultData.targetPoint);

        new AbilitySequenceHandler(activeAbility.AbilityCommandQueue, commonCommandData, optionalCommandData, EndPlayerActionPhase).Begin();

    }

    public void BeginPlayerActionPhase(BaseCharacterMaster activeCharacter, Action onPhaseCompleted)
    {
        onActionPhaseCompleted = onPhaseCompleted;
        this.activeCharacter = activeCharacter;

        if(activeCharacter.Unit.UnitState.IsTaunted(out var tauntEffect))
        {
            var tauntTargetUnit = UnitManager.Instance.GetActiveCharacter(tauntEffect.tauntTarget.GetUnitIdentifier());
            if (tauntTargetUnit != null)
            {
                TauntInfo.SetActive(true);
                DOVirtual.DelayedCall(2.5f, () => TauntInfo.SetActive(false));

                activeAbility = activeCharacter.Unit.UnitData.UnitAbilities.Find((ua) => ua.AbilityName == "Attack") ?? activeCharacter.Unit.UnitData.UnitAbilities.Random();
                OnTargetsChosen(new TargetingResultData(Vector3.zero, new List<UnitIdentifier> { tauntEffect.OptionalTarget.GetUnitIdentifier() }));
                return;
            }
        }

        activeCharacter.SetHighlight(true);

        SetActionMode(ActionMode.HUD);
    }

    public void EndPlayerActionPhase()
    {
        activeCharacter.SetHighlight(false);
        this.activeCharacter = null;
        SetActionMode(ActionMode.Disabled);
        onActionPhaseCompleted?.Invoke();
    }

    private List<ActionSelectionEntryData> GetBasicActions()
    {
        var results = new List<ActionSelectionEntryData>();

        foreach(var ability in activeCharacter.Unit.UnitData.UnitAbilities)
        {
            if(ability.Independant)
            {
                results.Add(new ActionSelectionEntryData(ability.AbilityName, () => SetActionMode(ActionMode.Targeting, ability), null));
            }
        }

        results.Add(new ActionSelectionEntryData("Ability", () => SetActionMode(ActionMode.PickAbility), null));
        results.Add(new ActionSelectionEntryData("Quit", () => Application.Quit(), null));

        return results;
    }

    private List<ActionSelectionEntryData> GetAbilityActions()
    {
        var results = new List<ActionSelectionEntryData>();

        foreach (var ability in activeCharacter.Unit.UnitData.UnitAbilities)
        {
            if (ability.Independant)
            {
                continue;
            }

            results.Add(new ActionSelectionEntryData(ability.AbilityName, () => SetActionMode(ActionMode.Targeting, ability), null));
        }

        results.Add(new ActionSelectionEntryData("Back", () => SetActionMode(ActionMode.HUD), null));

        return results;
    }

    private List<ActionSelectionEntryData> GetTargetingActions()
    {
        var results = new List<ActionSelectionEntryData>();

        results.Add(new ActionSelectionEntryData("Back", () => SetActionMode(ActionMode.HUD), null));

        return results;
    }
}