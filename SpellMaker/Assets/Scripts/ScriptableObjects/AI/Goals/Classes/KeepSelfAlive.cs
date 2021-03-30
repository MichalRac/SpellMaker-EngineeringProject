using UnityEngine;
/*
// Replace Template with proper names then extract method into new file to create new goal scriptable object class quickly
[CreateAssetMenu(fileName = "TemplateGoal", menuName = "ScriptableObjects/AI/Goals/TemplateGoal")]
public class TemplateGoal : Goal
{
}

 */

[CreateAssetMenu(fileName = "KeepSelfAlive", menuName = "ScriptableObjects/AI/Goals/KeepSelfAlive")]
public class KeepSelfAlive : Goal
{
     public override float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        if (!unit.UnitState.IsAlive)
            return 0f;

        return ProcessDiscontentmentValue(MAX_GOAL_URGENCY - (float)unit.UnitState.CurrentHp / unit.UnitData.MaxHp * MAX_GOAL_URGENCY);
    }

}
