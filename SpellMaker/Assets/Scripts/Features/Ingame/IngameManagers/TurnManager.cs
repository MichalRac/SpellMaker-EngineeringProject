using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Manage turns
    public static int turn = 0;
    public Queue<UnitIdentifier> queue { get; private set; } = new Queue<UnitIdentifier>() ;
    public UnitIdentifier active { get; private set; }

    public void PrepareQueue(Dictionary<UnitIdentifier, BaseCharacterMaster> allUnits)
    {
        var simpleListOfUnits = new List<UnitIdentifier>();

        foreach(var unit in allUnits.Keys)
        {
            simpleListOfUnits.Add(unit);
        }

        simpleListOfUnits.Shuffle();

        queue = simpleListOfUnits.ToQueue();
    }

    public UnitIdentifier GetNextInQueue()
    {
        if(active != null) // TODO Can probably be avoided
        {
            queue.Enqueue(active);
        }
        active = queue.Dequeue();
        return active;
    }

    public void UpdateStatus(List<UnitIdentifier> unitsToRemove)
    {
        var newQueue = new Queue<UnitIdentifier>();
        foreach (var unit in queue)
        {
            if (!unitsToRemove.Contains(unit))
                newQueue.Enqueue(unit);
        }
        if(unitsToRemove.Contains(active))
        {
            active = null;
        }
        queue = newQueue;
    }

    public void AddToQueue(UnitIdentifier unitIdentifier)
    {
        queue.Enqueue(unitIdentifier);
    }
}
