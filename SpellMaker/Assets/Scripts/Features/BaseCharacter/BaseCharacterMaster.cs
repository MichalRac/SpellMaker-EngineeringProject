using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMaster : MonoBehaviour, IUnit
{
    [SerializeField] private BaseCharacterPresenter baseCharacterPresenter;
    [SerializeField] private float characterSpeed = 5f;
    public UnitData unitData { get; private set; }

    // TODO cache this
    public Transform GetTransform() => GetComponent<Transform>();

    public void SetHighlight(bool value)
    {
        baseCharacterPresenter.SetHighlight(value);
    }

    public void SetSelect(bool value)
    {
        baseCharacterPresenter.SetSelect(value);
    }

    public void Initialize(UnitData data)
    {
        unitData = data;
        baseCharacterPresenter.Initialize(data);
    }

    public void TriggerMoveToEmpty(Vector3 target, Action onMovementFinishedCallback)
    {
        StartCoroutine(MoveTowards(target, onMovementFinishedCallback));
    }

    // TODO replace with NavMesh
    private IEnumerator MoveTowards(Vector3 target, Action onMovementFinishedCallback)
    {
        baseCharacterPresenter.SetWalkingAnim(true);

        transform.LookAt(target);

        while(Vector3.Magnitude(transform.position - target) > 0.1f)
        {
            var velocity = (target - transform.position).normalized * characterSpeed * Time.deltaTime;
            transform.position = transform.position + velocity;
            yield return null;
        }
        yield return null;

        baseCharacterPresenter.SetWalkingAnim(false);
        onMovementFinishedCallback?.Invoke();
    }

    public void TriggerMoveToUnit(IUnit target, Action onMovementFinishedCallback)
    {
        StartCoroutine(MoveTowardsUnit(target, onMovementFinishedCallback));
    }

    // TODO replace with NavMesh
    private IEnumerator MoveTowardsUnit(IUnit target, Action onMovementFinishedCallback)
    {
        baseCharacterPresenter.SetWalkingAnim(true);

        transform.LookAt(target.GetTransform());
        var targetPosition = target.GetTransform().position;

        while (Vector3.Magnitude(transform.position - targetPosition) > 1f)
        {
            var velocity = (targetPosition - transform.position).normalized * characterSpeed * Time.deltaTime;
            transform.position = transform.position + velocity;
            yield return null;
        }
        yield return null;

        baseCharacterPresenter.SetWalkingAnim(false);
        onMovementFinishedCallback?.Invoke();
    }

    public void TriggerAttackAnim(Action onCommandFinished)
    {
        baseCharacterPresenter.TriggerAttackAnim(onCommandFinished);
    }

    public void TriggerDamagedAnim(Action onCommandFinished)
    {
        baseCharacterPresenter.TriggerDamagedAnim(onCommandFinished);
    }

    public void ReciveDamage(int damage)
    {
        unitData.hp -= damage;
        baseCharacterPresenter.RefreshLabel(unitData);
    }

}
