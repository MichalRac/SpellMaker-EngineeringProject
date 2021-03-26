using UnityEngine;

[CreateAssetMenu(fileName = "KeepOpponentsTaunted", menuName = "ScriptableObjects/AI/Goals/KeepOpponentsTaunted")]
public class KeepOpponentsTaunted : Goal
{
    public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        var unitTeam = unit.UnitIdentifier.TeamId;
        int enemies = 0;
        int tauntedEnemies = 0;
        
        foreach (var modelUnit in worldModel.ModelActiveCharacters)
        {
            if (UnitHelpers.GetRelativeOwner(unitTeam, modelUnit.UnitIdentifier.TeamId) == UnitRelativeOwner.Opponent)
            {
                enemies++;

                if (modelUnit.UnitState.ActiveActionEffects.Exists((x) => x is TauntEffect))
                    tauntedEnemies++;
            }
        }

        if (enemies == 0)
        {
            return 0f;
        }
        
        return ProcessDiscontentmentValue(MAX_GOAL_URGENCY - (float)enemies / tauntedEnemies * MAX_GOAL_URGENCY);
    }

}
