using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseUnitAnimation
{
    void TriggerBaseAttackAnimation(Action onAttackConnectCallback, Action onAttackFinishedCallback);

    void SetWalkingAnimation(bool value);

    void TriggerDamagedAnimation(Action onDamagedAnimationFinished);

}
