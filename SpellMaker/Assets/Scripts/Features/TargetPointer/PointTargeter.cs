using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;

    public void Setup(AbilitySize abilitySize)
    {
        transform.gameObject.SetActive(true);
    }

    public List<BaseCharacterMaster> ExecuteTargeting()
    {
        return unitsInArea;
    }

    public void Move(Vector3 deltaPosition)
    {
        transform.position = transform.position + deltaPosition;
    }

    public void CancelTargeting()
    {
        transform.gameObject.SetActive(false);
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
}
