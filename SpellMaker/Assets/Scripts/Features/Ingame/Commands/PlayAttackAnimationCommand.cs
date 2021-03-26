using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayAttackAnimationCommand", menuName = "ScriptableObjects/Commands/Animation/PlayAttackAnimationCommand")]
public class PlayAttackAnimationCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        actor.TriggerAttackAnim(null, () =>
        {
            commandData.onCommandCompletedCallback?.Invoke();
        });
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
    }
}
