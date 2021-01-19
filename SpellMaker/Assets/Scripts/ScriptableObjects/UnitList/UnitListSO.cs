using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitListSO", menuName = "ScriptableObjects/Units/UnitListSO", order = 1)]
public class UnitListSO : ScriptableObject
{
    public List<UnitListData> unitAvatarDatas;

    public UnitListData GetUnitAvatar(UnitClass unitClass)
    {
        return unitAvatarDatas.Find((data) => data.unitClass == unitClass);
    }
}

[System.Serializable]
public class UnitListData
{
    public UnitClass unitClass;
    public UnitClassMaster unitClassMaster;
    public List<AbilitySetupSO> abilities;
}