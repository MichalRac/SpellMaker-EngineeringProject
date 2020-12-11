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
    [SerializeField] private GameObject Targeting;

    public void SetActionMode(ActionMode actionMode)
    {
        currentActionMode = actionMode;

        Targeting.SetActive(actionMode == ActionMode.Targeting);
    
        if(actionMode == ActionMode.HUD)
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
    }

    public void ExecuteNextAction()
    {
        enqueuedCommands.Dequeue().Execute(ExecuteNextAction);
    }

    public void EndPlayerActionPhase()
    {
        actionMakingActive = false;
        this.activeCharacter = null;
        SetActionMode(ActionMode.Disabled);
        onActionPhaseCompleted?.Invoke();
    }

    private List<ActionSelectionEntryData> GetBasicActions()
    {
        var results = new List<ActionSelectionEntryData>();

        results.Add(new ActionSelectionEntryData("attack", () => SetActionMode(ActionMode.Targeting), null));
        results.Add(new ActionSelectionEntryData("ability", () => SetActionMode(ActionMode.Targeting), null));
        results.Add(new ActionSelectionEntryData("quit", () => Application.Quit(), null));

        return results;
    }
}