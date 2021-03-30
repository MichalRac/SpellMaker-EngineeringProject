using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateParticlesFromTargetTowardOptionalTargetCommand", menuName = "ScriptableObjects/Commands/VFX/CreateParticlesFromTargetTowardOptionalTargetCommand")]
public class CreateParticlesFromTargetTowardOptionalTargetCommand : AbstractUnitCommand
{
    [SerializeField] private GameObject ParticlePrefab;
    [SerializeField] private float delayDuration = 1f;
    
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        var unit = UnitManager.Instance.GetActiveCharacter(commandData.actor);
        var particle = Instantiate(ParticlePrefab, unit.transform);
            
        var rotation = -Mathf.Atan2(unit.transform.position.z - optionalData.Position.z, unit.transform.position.x - optionalData.Position.x) * Mathf.Rad2Deg - 90;
        particle.transform.rotation = Quaternion.Euler(90f, rotation, 0f);

        DOVirtual.DelayedCall(delayDuration, () =>
        {
            commandData.onCommandCompletedCallback?.Invoke();
        });
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        commandData.onCommandCompletedCallback?.Invoke();
    }
}
