using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;
    private Quaternion rotation;
    [SerializeField] private SamDriver.Decal.DecalMesh decal;
    [SerializeField] private TargeterScaleSO targeterScaleSO;

    public List<BaseCharacterMaster> GetTargets()
    {
        return unitsInArea;
    }

    public void Setup(AbilitySize abilitySize)
    {
        var targeterScale = targeterScaleSO.GetAbilityTargeterScale(abilitySize);
        transform.parent.localScale = new Vector3(targeterScale, 1f, targeterScale);
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

    private void OnTriggerEnter(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null)
        {
            unitsInArea.Add(bcm);
            bcm.SetHighlight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var bcm = other.GetComponent<BaseCharacterMaster>();
        if (bcm != null && unitsInArea.Contains(bcm))
        {
            unitsInArea.Remove(bcm);
            bcm.SetHighlight(false);
        }
    }

    private IEnumerator UpdateDecal()
    {
        while(true)
        {
            var newRotation = transform.rotation;
            if(newRotation != rotation)
            {
                decal.GenerateProjectedMeshImmediate();
            }
            rotation = newRotation;
            yield return null;
        }
    }

    public void Move(Vector3 target)
    {
        transform.position = target;
    }
}
