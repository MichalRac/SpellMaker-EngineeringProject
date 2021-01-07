using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitListSO", menuName = "ScriptableObjects/Units/UnitListSO", order = 1)]
public class UnitListSO : ScriptableObject
{
    public List<Unit> unitDatas;
}
