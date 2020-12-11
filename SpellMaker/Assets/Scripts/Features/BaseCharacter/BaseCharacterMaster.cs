using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMaster : MonoBehaviour, IUnit
{
    [SerializeField] private BaseCharacterPresenter baseCharacterPresenter;
    [SerializeField] private float characterSpeed = 5f;
    public UnitData unitData { get; private set; }

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

    public void StartMovement(Vector3 target, Action onMovementFinishedCallback)
    {
        StartCoroutine(MoveTowards(target, onMovementFinishedCallback));
    }

    private IEnumerator MoveTowards(Vector3 target, Action onMovementFinishedCallback)
    {
        baseCharacterPresenter.SetWalkingAnim(true);

        while(Vector3.Magnitude(transform.position - target) > float.Epsilon)
        {
            var velocity = (transform.position - target).normalized * characterSpeed * Time.deltaTime;
            transform.position = transform.position + velocity;
            yield return null;
        }
        yield return null;

        baseCharacterPresenter.SetWalkingAnim(false);
        onMovementFinishedCallback?.Invoke();
    }
}
