using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand 
{
    void Execute(CommonCommandData commandData, OptionalCommandData optionalData = null);

    float GetGoalChange(Goal goal);
}
