using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineTargeter : MonoBehaviour, ITargeter
{
    private int CurrentTeamId { get; set; }
    private List<BaseCharacterMaster> unitsInArea;
    private Quaternion rotation;
    private List<UnitRelativeOwner> targetGroup;
    [SerializeField] private SamDriver.Decal.DecalMesh decal;
    [SerializeField] private TargeterScaleSO targeterScaleSO;

    private Vector3 basicTargeterPlacement;
    private float targetRotation;

    public void Setup(int currentTeamId, Vector3 initPos, float abilitySize, List<UnitRelativeOwner> targetGroup)
    {
        this.targetGroup = targetGroup;
        basicTargeterPlacement = initPos;
        targetRotation = transform.parent.rotation.y;
        transform.parent.position = new Vector3(initPos.x, transform.parent.position.y, initPos.z);
        transform.parent.localScale = new Vector3(abilitySize, 1f, transform.parent.localScale.z);
        transform.parent.gameObject.SetActive(true);
        CurrentTeamId = currentTeamId;
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

    public TargetingResultData ExecuteTargeting()
    {
        var unitsIdentifiers = unitsInArea.GetUnitIdentifiers();
        return new TargetingResultData(transform.position, unitsIdentifiers);
    }

    private void OnEnable()
    {
        unitsInArea = new List<BaseCharacterMaster>();
        rotation = transform.rotation;
        decal.GenerateProjectedMeshImmediate();
        StartCoroutine(UpdateDecal());
        StartCoroutine(AdjustRotation());
    }

    private void OnDisable()
    {
        unitsInArea.Clear();
        StopCoroutine(UpdateDecal());
        StopCoroutine(AdjustRotation());
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
        basicTargeterPlacement += displacement;

        targetRotation = -Mathf.Atan2(transform.parent.position.z - basicTargeterPlacement.z, transform.parent.position.x - basicTargeterPlacement.x) * Mathf.Rad2Deg - 90;
        
        transform.parent.rotation = Quaternion.Euler(0f, targetRotation, 0f);
    }

    public IEnumerator AdjustRotation()
    {

        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
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
