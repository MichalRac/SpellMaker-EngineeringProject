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
}
