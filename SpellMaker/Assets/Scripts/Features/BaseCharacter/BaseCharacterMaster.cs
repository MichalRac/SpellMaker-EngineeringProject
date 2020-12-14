﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMaster : MonoBehaviour, IUnit
{
    private const float STOP_AT_TARGET_DISTANCE = 0.05f;
    private const float STOP_BEFORE_TARGET_DISTANCE = 1.1f;

    [SerializeField] private BaseUnitPresenter baseUnitPresenter;
    [SerializeField] private UnitAnimationController unitAnimationController;
    [SerializeField] private float characterSpeed = 5f;
    public UnitData unitData { get; private set; }

    // TODO cache this
    public Transform GetTransform() => GetComponent<Transform>();

    public void SetHighlight(bool value)
    {
        baseUnitPresenter.SetHighlight(value);
    }

    public void SetSelect(bool value)
    {
        baseUnitPresenter.SetSelect(value);
    }

    public void Initialize(UnitData data)
    {
        unitData = data;
        baseUnitPresenter.Initialize(data);
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
        unitAnimationController.SetWalkingAnimation(true);

        transform.LookAt(target);

        while(Vector3.Magnitude(transform.position - target) > stopDistance)
        {
            var velocity = (target - transform.position).normalized * characterSpeed * Time.deltaTime;
            transform.position = transform.position + velocity;
            yield return null;
        }
        yield return null;

        unitAnimationController.SetWalkingAnimation(false);
        onMovementFinishedCallback?.Invoke();
    }
    public void TriggerAttackAnim(Action onCommandFinished, Action onAttackConnect)
    {
        unitAnimationController.TriggerBaseAttackAnimation(onAttackConnect, onCommandFinished);
    }

    public void TriggerDamagedAnim(Action onCommandFinished)
    {
        unitAnimationController.TriggerDamagedAnimation(onCommandFinished);
    }

    public void ReciveDamage(int damage)
    {
        unitData.hp -= damage;
        baseUnitPresenter.RefreshLabel(unitData);
    }

}
