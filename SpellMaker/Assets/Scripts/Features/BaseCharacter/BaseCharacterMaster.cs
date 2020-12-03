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

    // Start is called before the first frame update
    void Start()
    {
        Initialize(new UnitData() 
        { 
            characterId = 0,
            color = Color.blue,
            hp = 30 
        });
    }

    public void Initialize(UnitData data)
    {
        baseCharacterPresenter.Initialize(data);
    }
}
