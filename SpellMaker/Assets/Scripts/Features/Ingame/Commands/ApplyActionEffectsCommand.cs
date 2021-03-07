using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyActionEffectsCommand", menuName = "ScriptableObjects/Commands/Damage/ApplyActionEffectsCommand")]
public class ApplyActionEffectsCommand : AbstractUnitCommand
{
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        foreach (var targetIdentifier in commandData.targetsIdentifiers)
        {
            var target = UnitManager.Instance.GetActiveCharacter(targetIdentifier);

            foreach (var effect in commandData.unitAbility.AbilityEffects)
            {
                if(effect is TauntEffect tauntEffect)
                {
                    tauntEffect.OptionalTarget = UnitManager.Instance.GetActiveCharacter(commandData.actor);
                    tauntEffect.tauntTarget = tauntEffect.OptionalTarget;
                    target.Unit.UnitState.AddActionEffect(effect);
                    target.RefreshLabel();
                    continue;
                }

                target.Unit.UnitState.AddActionEffect(effect);
                effect.Affect(target, false);
            }
        }
        commandData.onCommandCompletedCallback?.Invoke();
    }
}
