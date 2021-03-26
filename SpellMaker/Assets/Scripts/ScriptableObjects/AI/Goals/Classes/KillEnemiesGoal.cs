using UnityEngine;
/*
// Replace Template with proper names then extract method into new file to create new goal scriptable object class quickly
[CreateAssetMenu(fileName = "TemplateGoal", menuName = "ScriptableObjects/AI/Goals/TemplateGoal")]
public class TemplateGoal : Goal
{
}

 */

[CreateAssetMenu(fileName = "KillEnemiesGoal", menuName = "ScriptableObjects/AI/Goals/KillEnemiesGoal")]
public class KillEnemiesGoal : Goal
{
     public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        var unitTeam = unit.UnitIdentifier.TeamId;
        int enemies = 0;
        int aliveEnemies = 0;
        
        foreach (var modelUnit in worldModel.ModelActiveCharacters)
        {
            if (UnitHelpers.GetRelativeOwner(unitTeam, modelUnit.UnitIdentifier.TeamId) == UnitRelativeOwner.Opponent)
            {
                enemies++;

                if (modelUnit.UnitState.IsAlive)
                    aliveEnemies++;
            }
        }

        if (enemies == 0)
        {
            return ProcessDiscontentmentValue(0f);
        }
        
        return ProcessDiscontentmentValue((float)aliveEnemies / enemies * MAX_GOAL_URGENCY);
    }

}
