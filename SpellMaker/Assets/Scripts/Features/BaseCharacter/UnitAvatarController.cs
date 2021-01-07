using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAvatarController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;

    public void SetTeamColor(UnitOwner owner)
    {
        characterMeshRenderer.material.color = owner == UnitOwner.Player ? Color.green : Color.red;
    }
}
