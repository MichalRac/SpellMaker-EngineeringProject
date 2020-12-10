using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Manage turns
    public static int turn = 0;
    Queue<UnitIdentifier> queue = new Queue<UnitIdentifier>();

    public void PrepareQueue(Dictionary<UnitOwner, List<BaseCharacterMaster>> allUnitLists)
    {
        var simpleListOfUnits = new List<UnitIdentifier>();

        foreach(var team in allUnitLists)
        {
            for(int i = 0; i < team.Value.Count; i++)
            {
                simpleListOfUnits.Add(new UnitIdentifier(team.Key, i));
            }
        }

        queue = simpleListOfUnits.ToQueue();
    }

    public UnitIdentifier GetNextInQueue()
    {
        var next = queue.Dequeue();
        queue.Enqueue(next);
        return next;
    }
}
