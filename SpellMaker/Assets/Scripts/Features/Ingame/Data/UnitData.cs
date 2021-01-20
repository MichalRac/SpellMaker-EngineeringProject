using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    public UnitIdentifier unitIdentifier;
    public UnitData unitData;

    public Unit(UnitIdentifier unitIdentifier, UnitData unitData)
    {
        this.unitIdentifier = unitIdentifier;
        this.unitData = unitData;
    }
}

[Serializable]
public class UnitIdentifier : IEquatable<UnitIdentifier>
{
    public UnitOwner owner;
    public int uniqueId;
    public UnitClass unitClass;

    public UnitIdentifier(UnitOwner owner, int uniqueId, UnitClass unitClass)
    {
        this.owner = owner;
        this.uniqueId = uniqueId;
        this.unitClass = unitClass;
    }

    public bool Equals(UnitIdentifier other)
    {
        return owner == other.owner && uniqueId == other.uniqueId;
    }

    public override string ToString()
    {
        return $"ID(owner-{owner}, id-{uniqueId})";
    }
}

[Serializable]
public class UnitData
{
    public int hp;
    public Color color;

    public UnitData(int hp, Color color)
    {
        this.hp = hp;
        this.color = color;
    }
}

public static class UnitFactory
{
    public static Unit GetUnit(UnitOwner owner, int uniqueId, int hp, Color color, UnitClass unitClass, List<UnitAbility> unitAbilities)
    {
        return new Unit(
            new UnitIdentifier(owner, uniqueId, unitClass), 
            new UnitData(hp, color)
            );    
    }

    public static Unit GetUnit(UnitOwner owner, int uniqueId, UnitClass unitClass, UnitData unitData)
    {
        return new Unit(new UnitIdentifier(owner, uniqueId, unitClass), unitData);
    }

    public static Unit GetUnit(UnitIdentifier unitIdentifier, UnitDataSO unitDataSO)
    {
        return new Unit(unitIdentifier, new UnitData(50, unitIdentifier.owner == UnitOwner.Player ? Color.green : Color.red));
    }

    public static Unit GetBasicUnit(UnitOwner owner, int uniqueId)
    {
        return GetUnit(owner, uniqueId, default, new UnitData(50, owner == UnitOwner.Player ? Color.blue : Color.black));
    }
}