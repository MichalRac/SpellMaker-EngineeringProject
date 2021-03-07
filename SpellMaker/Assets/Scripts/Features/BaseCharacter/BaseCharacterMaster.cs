using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMaster : MonoBehaviour, IUnit
{
    private const float STOP_AT_TARGET_DISTANCE = 0.05f;
    private const float STOP_BEFORE_TARGET_DISTANCE = 1.1f;

    [SerializeField] private BaseUnitPresenter baseUnitPresenter;
    [SerializeField] private UnitClassMaster unitClassMaster;
    [SerializeField] private float characterSpeed = 5f;
    public Unit Unit { get; private set; }

    // TODO cache this
    public Transform GetTransform() => GetComponent<Transform>();

    public void ActivateActionEffects()
    {
        for(int i = Unit.UnitState.ActiveActionEffects.Count - 1; i >= 0; i--)
        {
            Unit.UnitState.ActiveActionEffects[i].Affect(this, true);
            if(Unit.UnitState.ActiveActionEffects[i].IsFinished())
            {
                Unit.UnitState.ActiveActionEffects[i].OnRemoved(this);
                Unit.UnitState.ActiveActionEffects.RemoveAt(i);
            }
        }
    }

    public void SetHighlight(bool value)
    {
        baseUnitPresenter.SetHighlight(value);
    }

    public void SetSelect(bool value)
    {
        baseUnitPresenter.SetSelect(value);
    }

    public void SetTargeted(bool value)
    {
        baseUnitPresenter.SetTargeted(value);
    }

    public void ToggleShieldVisuals(bool value)
    {
        unitClassMaster.ToggleShieldColor(value);
    }

    public void RefreshLabel()
    {
        baseUnitPresenter.RefreshLabel(Unit);
    }

    public void Initialize(Unit data, UnitClassMaster unitClassMaster)
    {
        Unit = data;
        this.unitClassMaster = Instantiate(unitClassMaster, transform);
        baseUnitPresenter.Initialize(data, this.unitClassMaster);
    }

    public void TriggerMoveToEmpty(Vector3 target, Action onMovementFinishedCallback)
    {
        StartCoroutine(MoveTowards(target, STOP_AT_TARGET_DISTANCE, onMovementFinishedCallback));
    }

    public void TriggerMoveToUnit(IUnit target, Action onMovementFinishedCallback)
    {
        StartCoroutine(MoveTowards(target.GetTransform().position, STOP_BEFORE_TARGET_DISTANCE, onMovementFinishedCallback));
    }

    // TODO replace with NavMesh
    private IEnumerator MoveTowards(Vector3 target, float stopDistance, Action onMovementFinishedCallback)
    {
        unitClassMaster.UnitAnimationController.SetWalkingAnimation(true);

        transform.LookAt(target);

        while(Vector3.Magnitude(transform.position - target) > stopDistance)
        {
            var velocity = (target - transform.position).normalized * characterSpeed * Time.deltaTime;
            transform.position = transform.position + velocity;
            yield return null;
        }
        yield return null;

        unitClassMaster.UnitAnimationController.SetWalkingAnimation(false);
        onMovementFinishedCallback?.Invoke();
    }
    public void TriggerAttackAnim(Action onCommandFinished, Action onAttackConnect)
    {
        unitClassMaster.UnitAnimationController.TriggerBaseAttackAnimation(onAttackConnect, onCommandFinished);
    }

    public void TriggerDamagedAnim(Action onCommandFinished)
    {
        unitClassMaster.UnitAnimationController.TriggerDamagedAnimation(onCommandFinished);
    }

    public void TriggerDeathAnim(Action onCommandFinished)
    {
        unitClassMaster.UnitAnimationController.TriggerDeathAnimation(onCommandFinished);
    }

    public void ReciveDamage(int damage)
    {
        if (damage == 0)
        {
            return;
        }

        Unit.UnitState.ApplyDamage(damage);
        baseUnitPresenter.RefreshLabel(Unit);

        if(Unit.UnitState.IsAlive)
        {
            TriggerDamagedAnim(null);
        }
        else
        {
            TriggerDeathAnim(null);
        }
    }

    public void ReciveHeal(int healValue)
    {
        Unit.ApplyHeal(healValue);
        baseUnitPresenter.RefreshLabel(Unit);
    }
}
