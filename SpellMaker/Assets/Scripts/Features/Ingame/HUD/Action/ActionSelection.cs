using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelection : MonoBehaviour
{
    [SerializeField] private ActionSelectionEntry actionSelectionEntryPrefab;
    [SerializeField] private Transform actionSelectionsHolder;

    public void Setup(List<ActionSelectionEntryData> actionSelectionEntries)
    {
        foreach(var actionSelectionEntryData in actionSelectionEntries)
        {
            Instantiate(actionSelectionEntryPrefab, actionSelectionsHolder).Setup(actionSelectionEntryData);
        }
    }

    public void Discard()
    {
        actionSelectionsHolder.DestroyAllChildren();
    }
}
