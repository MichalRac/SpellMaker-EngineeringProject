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

    public UnitIdentifier(UnitOwner owner, int uniqueId)
    {
        this.owner = owner;
        this.uniqueId = uniqueId;
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
    public UnitClass unitClass;

    public UnitData(int hp, Color color, UnitClass unitClass)
    {
        this.hp = hp;
        this.color = color;
        this.unitClass = unitClass;
    }
}

public static class UnitFactory
{
    public static Unit GetUnit(UnitOwner owner, int uniqueId, int hp, int characterId, Color color, UnitClass unitClass, List<UnitAbility> unitAbilities)
    {
        return new Unit(
            new UnitIdentifier(owner, uniqueId), 
            new UnitData(hp, color, unitClass)
            );    
    }

    public static Unit GetUnit(UnitOwner owner, int uniqueId, UnitData unitData)
    {
        return new Unit(new UnitIdentifier(owner, uniqueId), unitData);
    }

    public static Unit GetBasicUnit(UnitOwner owner, int uniqueId)
    {
        return GetUnit(owner, uniqueId, new UnitData(50, owner == UnitOwner.Player ? Color.blue : Color.black, default));
    }
}