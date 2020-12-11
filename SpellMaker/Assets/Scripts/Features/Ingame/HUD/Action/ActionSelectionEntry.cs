using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ActionSelectionEntryData
{
    public string actionName;
    public UnityAction onActionPicked;
    public Sprite actionIcon;

    public ActionSelectionEntryData(string actionName, UnityAction onActionPicked, Sprite actionIcon)
    {
        this.actionName = actionName;
        this.onActionPicked = onActionPicked;
        this.actionIcon = actionIcon;
    }
}

public class ActionSelectionEntry : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI actionName;
    [SerializeField] public Image actionImage;
    [SerializeField] public Button button;

    public void Setup(ActionSelectionEntryData entryData)
    {
        gameObject.name += $"- {entryData.actionName}";
        actionName.text = entryData.actionName;
        button.onClick.AddListener(entryData.onActionPicked);
        if (entryData.actionIcon != null)
            actionImage.sprite = entryData.actionIcon;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

}
