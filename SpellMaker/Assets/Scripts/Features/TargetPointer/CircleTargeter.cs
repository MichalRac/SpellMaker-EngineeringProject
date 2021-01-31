﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;
    private Vector3 position;
    private List<UnitOwner> targetGroup;
    [SerializeField] private SamDriver.Decal.DecalMesh decal;
    [SerializeField] private TargeterScaleSO targeterScaleSO;

    public TargetingResultData ExecuteTargeting()
    {
        transform.parent.gameObject.SetActive(false);
        return new TargetingResultData(transform.position, unitsInArea.GetUnitIdentifiers());
    }

    public void Setup(Vector3 initPos, AbilitySize abilitySize, List<UnitOwner> targetGroup)
    {
        transform.parent.gameObject.SetActive(true);
        transform.parent.position = new Vector3(initPos.x, transform.position.y, initPos.z);
        var targeterScale = targeterScaleSO.GetAbilityTargeterScale(abilitySize);
        transform.parent.localScale = new Vector3(targeterScale, 1f, targeterScale);
        this.targetGroup = targetGroup;
    }

    private void OnEnable()
    {
        unitsInArea = new List<BaseCharacterMaster>();
        position = transform.position;
        decal.GenerateProjectedMeshImmediate();
        StartCoroutine(UpdateDecal());
    }

    private void OnDisable()
    {
        unitsInArea.Clear();
        StopCoroutine(UpdateDecal());
    }

    private IEnumerator UpdateDecal()
    {
        while(true)
        {
            var newPosition = transform.position;
            if(newPosition != position)
            {
                decal.GenerateProjectedMeshImmediate();
            }
            position = newPosition;
            yield return null;
        }
    }

    public void Move(Vector3 deltaPosition)
    {
        transform.parent.position = transform.parent.position + deltaPosition;
    }

    public void CancelTargeting()
    {
        foreach (var unit in unitsInArea)
        {
            unit.SetTargeted(false);
        }

        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null)
        {
            if (!targetGroup.Contains(bcm.Unit.unitIdentifier.owner))
            {
                return;
            }
            unitsInArea.Add(bcm);
            bcm.SetTargeted(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null && unitsInArea.Contains(bcm))
        {
            unitsInArea.Remove(bcm);
            bcm.SetTargeted(false);
        }
    }
}
