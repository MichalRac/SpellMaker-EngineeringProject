using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WinGoal", menuName = "ScriptableObjects/AI/Goals/WinGoal")]
public class WinGoal : Goal
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
