using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitListSO", menuName = "ScriptableObjects/Units/UnitListSO", order = 1)]
public class UnitAvatarListSO : ScriptableObject
{
    public List<UnitAvatarData> unitAvatarDatas;

    public UnitAvatarData GetUnitAvatar(UnitClass unitClass)
    {
        return unitAvatarDatas.Find((data) => data.unitClass == unitClass);
    }
}

[System.Serializable]
public class UnitAvatarData
{
    public UnitClass unitClass;
    public UnitAnimationController unitAnimationController;
}