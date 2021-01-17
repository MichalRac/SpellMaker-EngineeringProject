using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTargeter : MonoBehaviour, ITargeter
{
    private List<BaseCharacterMaster> unitsInArea;

    public List<BaseCharacterMaster> GetTargets()
    {
        return unitsInArea;
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
