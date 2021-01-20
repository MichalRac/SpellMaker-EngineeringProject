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
    public int baseDamage;

    public Color color;
    public List<UnitAbility> unitAbilities;

    public UnitData(UnitDataSO unitDataSO, List<UnitAbility> unitAbilities, Color color)
    {
        this.hp = unitDataSO.health;
        this.baseDamage = unitDataSO.baseDamage;
        this.color = color;
        this.unitAbilities = unitAbilities;
    }
}

public static class UnitFactory
{
    public static Unit GetUnit(UnitIdentifier unitIdentifier, UnitDataSO unitDataSO)
    {
        return new Unit(unitIdentifier, 
            new UnitData(unitDataSO, UnitAbilityFactory.GetUnitAbilities(unitDataSO), unitIdentifier.owner == UnitOwner.Player ? Color.green : Color.red));
    }
}