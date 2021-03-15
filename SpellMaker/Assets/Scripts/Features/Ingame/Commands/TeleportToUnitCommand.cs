using UnityEngine;

[CreateAssetMenu(fileName = "TeleportToUnitCommand", menuName = "ScriptableObjects/Commands/Movement/TeleportToUnitCommand")]
public class TeleportToUnitCommand : AbstractUnitCommand
{
    private const float DISTANCE_AFTER_TELEPORT = 2f;
    
    private enum TeleportType
    {
        None = 0,

        InFront = 1,
        Behind = 2,
    }

    [SerializeField] private TeleportType teleportType;

    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalCommandData = null)
    {
        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
        var deltaPositionNormalized = (actor.transform.position - target.transform.position).normalized;
        deltaPositionNormalized = teleportType == TeleportType.InFront ? deltaPositionNormalized : -deltaPositionNormalized;

        actor.TriggerMoveToEmpty(target.transform.position + deltaPositionNormalized * DISTANCE_AFTER_TELEPORT, null, true);

        var actorRotation = -Mathf.Atan2(actor.transform.position.z - target.transform.position.z, actor.transform.position.x - target.transform.position.x) * Mathf.Rad2Deg - 90;
        actor.transform.rotation = Quaternion.Euler(0f, actorRotation, 0f);

        commandData.onCommandCompletedCallback?.Invoke();
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var currentWorldModel = WorldModelService.Instance.GetCurrentWorldModelLayer();
        var actor = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var target = UnitManager.Instance.GetActiveCharacter(commandData.targetsIdentifiers[0]);
        var deltaPositionNormalized = (actor.transform.position - target.transform.position).normalized;
        deltaPositionNormalized = teleportType == TeleportType.InFront ? deltaPositionNormalized : -deltaPositionNormalized;

        actor.Unit.UnitData.Position = target.transform.position + deltaPositionNormalized * DISTANCE_AFTER_TELEPORT;

        commandData.onCommandCompletedCallback?.Invoke();
    }
}
