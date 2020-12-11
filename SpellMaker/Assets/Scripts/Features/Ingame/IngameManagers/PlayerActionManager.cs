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
    }

    private bool actionMakingActive = false;
    private bool isHUDActive = false;

    BaseCharacterMaster activeCharacter;
    ActionMode currentActionMode;
    Queue<ICommand> enqueuedCommands;
    Action onActionPhaseCompleted;

    [SerializeField] private ActionSelection ActionSelection;
    [SerializeField] private TargetPointerMaster Targeting;

    public void SetActionMode(ActionMode actionMode)
    {
        currentActionMode = actionMode;

        if(actionMode == ActionMode.Targeting)
        {
            Targeting.gameObject.SetActive(true);
            Targeting.Setup(OnTargetFound);
        }
        else
        {
            Targeting.gameObject.SetActive(false);
        }

        if (actionMode == ActionMode.HUD)
        {
            ActionSelection.gameObject.SetActive(true);
            ActionSelection.Setup(GetBasicActions());
        }
        else
        {
            ActionSelection.gameObject.SetActive(false);
            ActionSelection.Discard();
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

    public void ExecuteNextAction()
    {
        SetActionMode(ActionMode.Disabled);

        if (enqueuedCommands.Count > 0)
        {
            enqueuedCommands.Dequeue().Execute(ExecuteNextAction);
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

        results.Add(new ActionSelectionEntryData("Attack", () => SetActionMode(ActionMode.Targeting), null));
        results.Add(new ActionSelectionEntryData("Ability", () => SetActionMode(ActionMode.Targeting), null));
        results.Add(new ActionSelectionEntryData("Quit", () => Application.Quit(), null));

        return results;
    }

    private void OnTargetFound(IUnit selectedUnit, Vector3 position)
    {
        if(selectedUnit == null)
        {
            enqueuedCommands.Enqueue(new MoveUnitCommand(activeCharacter, position));
            ExecuteNextAction();
        }
        else
        {
            enqueuedCommands.Enqueue(new MoveUpToUnitCommand(activeCharacter, selectedUnit));
            enqueuedCommands.Enqueue(new AttackUnitCommand(activeCharacter, selectedUnit));
            ExecuteNextAction();
        }
    }
}