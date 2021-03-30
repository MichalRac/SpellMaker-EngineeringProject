using UnityEngine;

[CreateAssetMenu(fileName = "DamageEnemiesGoal", menuName = "ScriptableObjects/AI/Goals/DamageEnemiesGoal")]
public class DamageEnemiesGoal : Goal
{
    public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        var unitTeam = unit.UnitIdentifier.TeamId;
        int totalHealth = 0;
        int currentHealth = 0;
        
        foreach (var modelUnit in worldModel.ModelActiveCharacters)
        {
            if (UnitHelpers.GetRelativeOwner(unitTeam, modelUnit.UnitIdentifier.TeamId) == UnitRelativeOwner.Opponent)
            {
                totalHealth += modelUnit.UnitData.MaxHp;
                currentHealth += modelUnit.UnitState.CurrentHp;
            }
        }

        if (totalHealth == 0)
        {
            return ProcessDiscontentmentValue(0f);
        }
        
        return ProcessDiscontentmentValue((float) currentHealth / totalHealth * MAX_GOAL_URGENCY);
    }
}
