using UnityEngine;

[CreateAssetMenu(fileName = "KeepTeamShieldedGoal", menuName = "ScriptableObjects/AI/Goals/KeepTeamShieldedGoal")]
public class KeepTeamShielded : Goal
{
    public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        var unitTeam = unit.UnitIdentifier.TeamId;
        int allies = 0;
        int shieldedAllies = 0;
        
        foreach (var modelUnit in worldModel.ModelActiveCharacters)
        {
            if (UnitHelpers.GetRelativeOwner(unitTeam, modelUnit.UnitIdentifier.TeamId) == UnitRelativeOwner.Ally)
            {
                allies++;

                if (modelUnit.UnitState.ActiveActionEffects.Exists((x) => x is ShieldEffect))
                    shieldedAllies++;
            }
        }

        if (allies == 0)
        {
            return ProcessDiscontentmentValue(0f);
        }
        
        return ProcessDiscontentmentValue(MAX_GOAL_URGENCY - (float)allies / shieldedAllies * MAX_GOAL_URGENCY);
    }

}