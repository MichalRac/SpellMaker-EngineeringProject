using UnityEngine;

[CreateAssetMenu(fileName = "WinGoal", menuName = "ScriptableObjects/AI/Goals/WinGoal")]
public class WinGoal : Goal
{

    public override float GetDiscontentmentChange<T>(T command, CommonCommandData ccd, OptionalCommandData ocd)
    {
        var result = 0f;

        switch(command)
        {
            case AttackUnitCommand attackCommand:
                
                break;
        }

        return result;
    }
}
