using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Manage turns
    public static int turn = 0;
    public Queue<UnitIdentifier> queue { get; private set; } = new Queue<UnitIdentifier>() ;
    public UnitIdentifier current { get; private set; }

    public void PrepareQueue(Dictionary<UnitIdentifier, BaseCharacterMaster> allUnits)
    {
        var simpleListOfUnits = new List<UnitIdentifier>();

        foreach(var unit in allUnits.Keys)
        {
            simpleListOfUnits.Add(unit);
        }

        queue = simpleListOfUnits.ToQueue();
        current = queue.Peek();
    }

    public UnitIdentifier GetNextInQueue()
    {
        queue.Enqueue(current);
        current = queue.Dequeue();
        return current;
    }
}
