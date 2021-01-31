using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;
    private Quaternion rotation;
    private List<UnitOwner> targetGroup;
    [SerializeField] private SamDriver.Decal.DecalMesh decal;
    [SerializeField] private TargeterScaleSO targeterScaleSO;

    private Vector3 targetDirection;

    public void Setup(Vector3 initPos, AbilitySize abilitySize, List<UnitOwner> targetGroup)
    {
        this.targetGroup = targetGroup;
        targetDirection = initPos;
        transform.parent.position = new Vector3(initPos.x, transform.parent.position.y, initPos.z);
        transform.parent.gameObject.SetActive(true);
    }

    public void CancelTargeting()
    {
        foreach (var unit in unitsInArea)
        {
            unit.SetTargeted(false);
        }

        transform.parent.gameObject.SetActive(false);
    }

    public TargetingResultData ExecuteTargeting()
    {
        transform.parent.gameObject.SetActive(false);
        return new TargetingResultData(transform.position, unitsInArea.GetUnitIdentifiers());
    }

    private void OnEnable()
    {
        unitsInArea = new List<BaseCharacterMaster>();
        rotation = transform.rotation;
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
        while (true)
        {
            var newRotation = transform.rotation;
            if (newRotation != rotation)
            {
                decal.GenerateProjectedMeshImmediate();
            }
            rotation = newRotation;
            yield return null;
        }
    }


    public void Move(Vector3 displacement)
    {
        targetDirection += displacement;
        var newRotation = -Mathf.Atan2(transform.parent.position.z - targetDirection.z, transform.parent.position.x - targetDirection.x) * Mathf.Rad2Deg - 90;
        transform.parent.rotation = Quaternion.Euler(0f, newRotation, 0f);
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
