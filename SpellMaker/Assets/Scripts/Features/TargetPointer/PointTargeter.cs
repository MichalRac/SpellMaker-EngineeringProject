using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;
    private List<UnitOwner> targetGroup;

    public void Setup(Vector3 initPos, AbilitySize abilitySize, List<UnitOwner> targetGroup)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(initPos.x, transform.position.y, initPos.z);
        this.targetGroup = targetGroup;
    }

    public TargetingResultData ExecuteTargeting()
    {
        gameObject.SetActive(false);
        return new TargetingResultData(transform.position, unitsInArea.GetUnitIdentifiers());
    }

    public void Move(Vector3 deltaPosition)
    {
        transform.position = transform.position + deltaPosition;
    }

    public void CancelTargeting()
    {
        gameObject.SetActive(false);

        foreach (var unit in unitsInArea)
        {
            unit.SetHighlight(false);
        }
        unitsInArea.Clear();

    }

    private void OnEnable()
    {
        unitsInArea = new List<BaseCharacterMaster>();
    }

    private void OnDisable()
    {
        unitsInArea.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null)
        {
            if(!targetGroup.Contains(bcm.Unit.unitIdentifier.owner))
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
            if (!targetGroup.Contains(bcm.Unit.unitIdentifier.owner))
            {
                return;
            }
            unitsInArea.Remove(bcm);
            bcm.SetTargeted(false);
        }
    }
}
