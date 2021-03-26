using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName = "ScriptableObjects/AI/Goal")]
public class Goal : ScriptableObject
{
    protected const float MAX_GOAL_URGENCY = 5f;
    [SerializeField] public float UrgencyPower;
    
    public virtual float GetDiscontentmentValue(Unit unit, WorldModel worldModel)
    {
        return ProcessDiscontentmentValue(0f);
    }
    
    public float ProcessDiscontentmentValue(float urgencyValue)
    {
        return urgencyValue * urgencyValue;
    }

}

/*
// Replace Template with proper names then extract method into new file to create new goal scriptable object class quickly
[CreateAssetMenu(fileName = "TemplateGoal", menuName = "ScriptableObjects/AI/Goals/TemplateGoal")]
public class TemplateGoal : Goal
{
}

 */