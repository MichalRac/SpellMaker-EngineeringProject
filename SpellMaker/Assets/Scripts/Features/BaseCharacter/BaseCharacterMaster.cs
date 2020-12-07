using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMaster : MonoBehaviour, IUnit
{
    [SerializeField] private BaseCharacterPresenter baseCharacterPresenter;

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
        baseCharacterPresenter.Initialize(data);
        transform.position = new Vector3(Random.Range(-9f, 9f), transform.position.y, Random.Range(-9f, 9f));
    }
}
