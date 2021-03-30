using UnityEngine;

[CreateAssetMenu(fileName = "AttackUnitCommand", menuName = "ScriptableObjects/Commands/Damage/AttackUnitCommand")]
public class AttackUnitCommand : AbstractUnitCommand
{
    // TODO this wouldn't be needed if there was a command for just animation handling
    [SerializeField] private bool applyBaseDamage;

    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        if (commandData.targetsIdentifiers.Count == 0)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with no targets");
            return;
        }

        if (commandData.targetsIdentifiers.Count > 1)
        {
            Debug.Log("[AttackUnitCommand] Attempting to attack multiple targets with single AttackUnitCommand");
        }

        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        actor.TriggerAttackAnim(null, () => 
        {
            commandData.onCommandCompletedCallback?.Invoke();

            if (commandData.targetsIdentifiers[0] == null)
            {
                Debug.LogError($"[AttackUnitCommand] Missing target in command data");
                return;
            }
            
            var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
            if (target != null)
            {
                target.ReciveDamage(applyBaseDamage ? actor.Unit.UnitData.BaseDamage : 0);
            }
        });
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var currentWorldModel = WorldModelService.Instance.GetCurrentWorldModelLayer();

        if (currentWorldModel.TryGetUnit(commandData.targetsIdentifiers[0], out var target))
        {
            if (applyBaseDamage)
            {
                if (currentWorldModel.TryGetUnit(commandData.actor, out var actor))
                {
                    target.UnitState.ApplyDamage(actor.UnitData.BaseDamage);
                }
                else
                {
                    Debug.LogError($"Couldn't find actor Unit with {commandData.actor} UnitIdentifier");
                }
            }
            else
            {
                target.UnitState.ApplyDamage(0);
            }
        }
        else
        {
            Debug.LogError($"Couldn't find target Unit with {commandData.targetsIdentifiers[0]} UnitIdentifier");
        }
        commandData.onCommandCompletedCallback?.Invoke();
    }
}
