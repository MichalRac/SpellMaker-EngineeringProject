using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName = "ScriptableObjects/AI/Goal")]
public class Goal : ScriptableObject
{
    [SerializeField] public float Urgency;

    public float GetCurrentDiscontentment()
    {
        return Mathf.Pow(Urgency, 2);
    }

    public float GetDeltaDiscontentment(float deltaUrgency)
    {
        return Mathf.Pow(Urgency + deltaUrgency, 2) - GetCurrentDiscontentment();
    }
}