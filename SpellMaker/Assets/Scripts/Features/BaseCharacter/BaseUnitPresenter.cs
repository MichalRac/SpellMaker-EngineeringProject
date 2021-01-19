using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TMPro;
using System;

public class BaseUnitPresenter : MonoBehaviour, IUnit
{
    // Start is called before the first frame update
    [SerializeField] private GameObject selectionProjector;
    [SerializeField] private GameObject highlightProjector;
    [SerializeField] private GameObject shadowProjector;

    [SerializeField] private TextMeshPro characterLabel;

    // TODO cache this
    public Transform GetTransform() => GetComponent<Transform>();

    public void Initialize(Unit unit, UnitClassMaster unitClassMaster)
    {
        unitClassMaster.SetTeamColor(unit.unitIdentifier.owner);
        Setup(unit);
    }

    public void Setup(Unit unit)
    {
        characterLabel.color = unit.unitData.color;
        characterLabel.text = $"ID: {unit.unitIdentifier.uniqueId}\nHP: {unit.unitData.hp}\nClass: {unit.unitData.unitClass}";
    }

    public void RefreshLabel(Unit unit)
    {
        characterLabel.text = unit.unitData.hp > 0 
            ? $"ID: {unit.unitIdentifier.uniqueId}\nHP: {unit.unitData.hp}\nClass: {unit.unitData.unitClass}"
            : "DEAD";
    }

    public void SetSelect(bool value)
    {
        selectionProjector.gameObject.SetActive(value);
    }

    public void SetHighlight(bool value)
    {
        highlightProjector.gameObject.SetActive(value);
        shadowProjector.gameObject.SetActive(!value);
    }
}
