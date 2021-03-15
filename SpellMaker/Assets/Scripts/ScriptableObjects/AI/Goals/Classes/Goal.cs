using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName = "ScriptableObjects/AI/Goal")]
public class Goal : ScriptableObject
{
    [SerializeField] public float UrgencyPower;

/*    public float GetCurrentDiscontentment()
    {
        return Mathf.Pow(Urgency, 2);
    }

    public float GetDeltaDiscontentment(float deltaUrgency)
    {
        return Mathf.Pow(Urgency + deltaUrgency, 2) - GetCurrentDiscontentment();
    }*/

    public virtual float GetDisconentment(float urgencyValue)
    {
        return urgencyValue * urgencyValue;
    }

    public virtual float GetDiscontentmentChange<T>(T command, CommonCommandData ccd, OptionalCommandData ocd) where T : AbstractUnitCommand
    {
        return 0f;
    }
}

/*
// Replace Template with proper names then extract method into new file to create new goal scriptable object class quickly
[CreateAssetMenu(fileName = "TemplateGoal", menuName = "ScriptableObjects/AI/Goals/TemplateGoal")]
public class TemplateGoal : Goal
{
}

 */