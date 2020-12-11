using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitData
{
    public UnitIdentifier unitIdentifier;

    public int hp;
    public int characterId;
    public Color color;
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
