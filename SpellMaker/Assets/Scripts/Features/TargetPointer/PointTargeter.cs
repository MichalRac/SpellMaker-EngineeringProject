using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;
    private List<UnitRelativeOwner> targetGroup;
    private int CurrentTeamId { get; set; }

    public void Setup(int currentTeamId, Vector3 initPos, AbilitySize abilitySize, List<UnitRelativeOwner> targetGroup)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(initPos.x, transform.position.y, initPos.z);
        this.targetGroup = targetGroup;
        CurrentTeamId = currentTeamId;
    }

    public TargetingResultData ExecuteTargeting()
    {
        var unitsIdentifiers = unitsInArea.GetUnitIdentifiers();
        return new TargetingResultData(transform.position, unitsIdentifiers);
    }

    public void Move(Vector3 deltaPosition)
    {
        transform.position = transform.position + deltaPosition;
    }

    public void SimpleRestart()
    {
        gameObject.SetActive(true);
    }

    public void CancelTargeting()
    {
        foreach (var unit in unitsInArea)
        {
            unit.SetHighlight(false);
            unit.SetTargeted(false);
        }
        unitsInArea.Clear();
        gameObject.SetActive(false);
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
            if( !targetGroup.Contains( UnitHelpers.GetRelativeOwner( CurrentTeamId, bcm.Unit.UnitIdentifier.TeamId ) ) )
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
            if (!targetGroup.Contains(UnitHelpers.GetRelativeOwner(CurrentTeamId, bcm.Unit.UnitIdentifier.TeamId)))
            {
                return;
            }
            unitsInArea.Remove(bcm);
            bcm.SetTargeted(false);
        }
    }
}
