using System;
using System.Collections;
using System.Collections.Generic;
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

    private bool actionMakingActive = false;
    private bool isHUDActive = false;

    BaseCharacterMaster activeCharacter;
    ActionMode currentActionMode;
    Queue<ICommand> enqueuedCommands;
    Action onActionPhaseCompleted;

    [SerializeField] private ActionSelection ActionSelection;
    [SerializeField] private TargeterMaster Targeting;

    public void SetActionMode(ActionMode actionMode, UnitAbility ability = null)
    {
        currentActionMode = actionMode;
        Targeting.CancelTargeting();

        switch (actionMode)
        {
            case ActionMode.HUD:
                Targeting.gameObject.SetActive(false);
                ActionSelection.gameObject.SetActive(true);
                ActionSelection.Setup(GetBasicActions());
                break;
            case ActionMode.Targeting:
                Targeting.gameObject.SetActive(true);
                Targeting.StartTargeting(activeCharacter.transform.position, ability, null);
                ActionSelection.Setup(GetTargetingActions());
                break;
            case ActionMode.PickAbility:
                Targeting.gameObject.SetActive(false);
                ActionSelection.gameObject.SetActive(true);
                ActionSelection.Setup(GetAbilityActions(activeCharacter.Unit.unitData));
                break;
        }
    }

    public void BeginPlayerActionPhase(BaseCharacterMaster activeCharacter, Action onPhaseCompleted)
    {
        actionMakingActive = true;

        SetActionMode(ActionMode.HUD);
        enqueuedCommands = new Queue<ICommand>();
        onActionPhaseCompleted = onPhaseCompleted;
        this.activeCharacter = activeCharacter;
        activeCharacter.SetHighlight(true);
    }

    public void ExecuteNextAction(CommonCommandData commonCommandData, OptionalCommandData optionalCommandData)
    {
        SetActionMode(ActionMode.Disabled);

        if (enqueuedCommands.Count > 0)
        {
            enqueuedCommands.Dequeue().Execute(commonCommandData, optionalCommandData);
        }
        else
        {
            EndPlayerActionPhase();
        }
    }

    public void EndPlayerActionPhase()
    {
        actionMakingActive = false;
        activeCharacter.SetHighlight(false);
        this.activeCharacter = null;
        onActionPhaseCompleted?.Invoke();
    }

    private List<ActionSelectionEntryData> GetBasicActions()
    {
        var results = new List<ActionSelectionEntryData>();

        results.Add(new ActionSelectionEntryData("Attack", () => SetActionMode(ActionMode.Targeting, GetAttackUnitAbility()), null));
        results.Add(new ActionSelectionEntryData("Ability", () => SetActionMode(ActionMode.PickAbility), null));
        results.Add(new ActionSelectionEntryData("Quit", () => Application.Quit(), null));

        return results;
    }

    private List<ActionSelectionEntryData> GetTargetingActions()
    {
        var results = new List<ActionSelectionEntryData>();

        results.Add(new ActionSelectionEntryData("Back", () => SetActionMode(ActionMode.HUD), null));

        return results;
    }

    private List<ActionSelectionEntryData> GetAbilityActions(UnitData activeUnitData)
    {
        var results = new List<ActionSelectionEntryData>();

        foreach (var ability in activeUnitData.unitAbilities)
        {
            results.Add(new ActionSelectionEntryData(ability.AbilityName, () => SetActionMode(ActionMode.Targeting, ability), null));
        }

        results.Add(new ActionSelectionEntryData("Back", () => SetActionMode(ActionMode.HUD), null));

        return results;
    }

    private void OnTargetsFound(List<BaseCharacterMaster> selectedUnits, Vector3 position)
    {
        if(selectedUnits == null || selectedUnits.Count == 0)
        {
            enqueuedCommands.Enqueue(new MoveUnitCommand());
        }
        else
        {
            enqueuedCommands.Enqueue(new MoveUpToUnitCommand());
            enqueuedCommands.Enqueue(new AttackUnitCommand());
        }
    }

    private UnitAbility GetAttackUnitAbility()
    {
        return new UnitAbility("attack",
                TargetingType.Single, AbilitySize.None,
                new List<ActionEffect>() { new DamageEffect(0, 30) },
                GetAttackCommandList(),
                new List<UnitOwner> { UnitOwner.Opponent });
    }

    private List<AbstractUnitCommand> GetAttackCommandList()
    {
        return new List<AbstractUnitCommand>()
        {
            new MoveUpToUnitCommand(),
            new AttackUnitCommand(),
        };
    }
}