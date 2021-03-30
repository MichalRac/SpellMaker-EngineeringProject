using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateParticlesOnTargetsCommand", menuName = "ScriptableObjects/Commands/VFX/CreateParticlesOnTargetsCommand")]
public class CreateParticlesOnTargetsCommand : AbstractUnitCommand
{
    [SerializeField] private GameObject ParticlePrefab;
    
    public override void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        foreach (var target in commandData.targetsIdentifiers)
        {
            var targetUnit = UnitManager.Instance.GetActiveCharacter(target);
            Instantiate(ParticlePrefab, targetUnit.transform);
        }
        commandData.onCommandCompletedCallback?.Invoke();
    }

    public override void Simulate(CommonCommandData commandData, OptionalCommandData optionalData = null)
    {
        commandData.onCommandCompletedCallback?.Invoke();
    }
}
