using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTargeter : MonoBehaviour, ITargeter
{
    private int CurrentTeamId { get; set; }
    private List<BaseCharacterMaster> unitsInArea;
    private Vector3 position;
    private List<UnitRelativeOwner> targetGroup;

    [SerializeField] private SamDriver.Decal.DecalMesh decal;
    [SerializeField] private TargeterScaleSO targeterScaleSO;

    public TargetingResultData ExecuteTargeting()
    {
        var unitsIdentifiers = unitsInArea.GetUnitIdentifiers();
        return new TargetingResultData(transform.position, unitsIdentifiers);
    }

    public void Setup(int currentTeamId, Vector3 initPos, float abilitySize, List<UnitRelativeOwner> targetGroup)
    {
        transform.parent.gameObject.SetActive(true);
        transform.parent.position = new Vector3(initPos.x, transform.position.y, initPos.z);
        transform.parent.localScale = new Vector3(abilitySize, 1f, abilitySize);
        CurrentTeamId = currentTeamId;
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
            unit.SetTargeted(false);
        }

        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null && bcm.Unit.UnitState.IsAlive)
        {
            if (!targetGroup.Contains(UnitHelpers.GetRelativeOwner(CurrentTeamId, bcm.Unit.UnitIdentifier.TeamId)))
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
