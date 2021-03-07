using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimationController : MonoBehaviour, IBaseUnitAnimation
{
    [SerializeField] private Animator animator;

    #region Attack Animation

    private readonly int attackAnimParam = Animator.StringToHash("Attack");
    private Action onAttackConnectCallback;
    private Action onAttackFinishedCallback;

    public void TriggerBaseAttackAnimation(Action onAttackConnectCallback, Action onAttackFinishedCallback)
    {
        this.onAttackConnectCallback = onAttackConnectCallback;
        this.onAttackFinishedCallback = onAttackFinishedCallback;

        animator.SetTrigger(attackAnimParam);
    }

    public void OnAttackConnect()
    {
        onAttackConnectCallback?.Invoke();
    }

    public void OnAttackFinished()
    {
        onAttackFinishedCallback?.Invoke();
    }

    #endregion

    #region Walking Animation

    private readonly int walkingAnimParam = Animator.StringToHash("Walking");

    public void SetWalkingAnimation(bool value)
    {
        animator.SetBool(walkingAnimParam, value);
    }

    #endregion

    #region Damaged Animation
    
    private readonly int damagedAnimParam = Animator.StringToHash("Damaged");

    private Action onDamagedAnimationFinished;

    public void TriggerDamagedAnimation(Action onDamagedAnimationFinished)
    {
        this.onDamagedAnimationFinished = onDamagedAnimationFinished;
        animator.SetTrigger(damagedAnimParam);
    }

    public void OnDamagedAnimationFinished()
    {
        onDamagedAnimationFinished?.Invoke();
    }

    #endregion

    #region Death Animation

    private readonly int deathAnimParam = Animator.StringToHash("Death");
    private Action onDeathAnimationFinished;


    public void TriggerDeathAnimation(Action onDeathAnimationFinished)
    {
        this.onDeathAnimationFinished = onDeathAnimationFinished;
        animator.SetTrigger(deathAnimParam);
    }

    public void OnDeathAnimationFinished()
    {
        onDeathAnimationFinished?.Invoke();
    }

    #endregion
}
