using System.Collections.Generic;

public class TurnAction
{
    public Queue<AbstractUnitCommand> QueueOfCommands { get; }
    public CommonCommandData CommonCommandData { get; }
    public OptionalCommandData OptionalCommandData { get; }

    public TurnAction(Queue<AbstractUnitCommand> queueOfCommands, CommonCommandData commonCommandData, OptionalCommandData optionalCommandData)
    {
        QueueOfCommands = queueOfCommands;
        CommonCommandData = commonCommandData;
        OptionalCommandData = optionalCommandData;
    }

    public float GetGoalChange(List<Goal> characterGoals, CommonCommandData ccd, OptionalCommandData ocd)
    {
        var totalChange = 0f;

        foreach(var command in QueueOfCommands)
        {
            totalChange += command.GetCommandDeltaDiscontentment(characterGoals, ccd, ocd);
        }

        return totalChange;
    }
}
