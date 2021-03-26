using UnityEngine;

[CreateAssetMenu(fileName = "KeepTeamHealthy", menuName = "ScriptableObjects/AI/Goals/KeepTeamHealthy")]
public class KeepTeamHealthy : Goal
{
     public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        var unitTeam = unit.UnitIdentifier.TeamId;
        int alliesTotalHealth = 0;
        int alliesCurrentHealth = 0;
        
        foreach (var modelUnit in worldModel.ModelActiveCharacters)
        {
            if (UnitHelpers.GetRelativeOwner(unitTeam, modelUnit.UnitIdentifier.TeamId) == UnitRelativeOwner.Ally)
            {
                alliesTotalHealth += modelUnit.UnitData.MaxHp;
                alliesCurrentHealth += modelUnit.UnitState.CurrentHp;
            }
        }

        if (alliesTotalHealth == 0)
        {
            return ProcessDiscontentmentValue(0f);
        }
        
        return ProcessDiscontentmentValue(MAX_GOAL_URGENCY - (float)alliesCurrentHealth / alliesTotalHealth * MAX_GOAL_URGENCY);
    }

}
