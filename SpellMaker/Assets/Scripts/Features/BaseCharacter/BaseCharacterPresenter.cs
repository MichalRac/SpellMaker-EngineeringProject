using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TMPro;

public class BaseCharacterPresenter : MonoBehaviour, IUnit
{
    // Start is called before the first frame update
    [SerializeField] private GameObject selectionProjector;
    [SerializeField] private GameObject highlightProjector;
    [SerializeField] private GameObject shadowProjector;
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;
    [SerializeField] private Material materialToSetup;

    [SerializeField] private TextMeshPro characterLabel;

    [SerializeField] private Animator animator;
    private readonly int walkingAnimParam = Animator.StringToHash("Walking");
    private readonly int attackAnimParam = Animator.StringToHash("Attack");


    public void Initialize(UnitData data)
    {
        Setup(data);
    }

    public void Setup(UnitData data)
    {
        characterMeshRenderer.material.color = data.color;
        characterLabel.color = data.color;
        characterLabel.text = $"ID: {data.unitIdentifier.uniqueId}\nHP: {data.hp}";
    }

    private void Buf_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
    {
        characterMeshRenderer.material = obj.Result;
    }

    public void SetSelect(bool value)
    {
        selectionProjector.gameObject.SetActive(value);
    }

    public void SetHighlight(bool value)
    {
        highlightProjector.gameObject.SetActive(value);
        shadowProjector.gameObject.SetActive(!value);
    }

    public void SetWalkingAnim(bool value)
    {
        animator.SetBool(walkingAnimParam, value);
    }

    public void TriggerAttackAnim()
    {
        animator.SetTrigger(attackAnimParam);
    }
}
