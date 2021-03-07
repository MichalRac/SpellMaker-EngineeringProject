using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalOrientedBehaviourPresetSO", menuName = "ScriptableObjects/AI/GoalOrientedBehaviourPresetSO")]
public class GoalOrientedBehaviourPresetSO : ScriptableObject
{
    [SerializeField] public List<Goal> goals;
}
