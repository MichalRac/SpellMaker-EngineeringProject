using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    public UnitIdentifier UnitIdentifier { get; }
    public UnitData UnitData { get; }
    public UnitState UnitState { get; }

    public Unit(UnitIdentifier unitIdentifier, UnitData unitData)
    {
        UnitIdentifier = unitIdentifier;
        UnitData = unitData;
        UnitState = new UnitState(unitData.MaxHp);
    }

    // TODO find a cleaner way to do it
    public void ApplyHeal(int value)
    {
        var maxHP = UnitData.MaxHp;
        UnitState.ApplyHeal(value);
        if(UnitState.CurrentHp > maxHP)
        {
            UnitState.ApplyDamage(UnitState.CurrentHp - maxHP);
        }
    }
}

[Serializable]
public class UnitIdentifier : IEquatable<UnitIdentifier>
{
    public int TeamId { get; private set; }
    public int UniqueId { get; private set; }
    public UnitClass UnitClass { get; private set; }

    public UnitIdentifier(int teamId, int uniqueId, UnitClass unitClass)
    {
        TeamId = teamId;
        UniqueId = uniqueId;
        UnitClass = unitClass;
    }

    // Only for match preparation
    public void SwitchClass(UnitClass newClass)
    {
        UnitClass = newClass;
    }

    public bool Equals(UnitIdentifier other)
    {
        return TeamId == other.TeamId && UniqueId == other.UniqueId;
    }

    public override string ToString()
    {
        return $"ID(team id-{TeamId}, id-{UniqueId})";
    }
}

[Serializable]
public class UnitData
{
    public int MaxHp { get; private set; }
    public int BaseDamage { get; }

    public Color Color { get; }
    public List<UnitAbility> UnitAbilities { get; }

    public UnitData(UnitDataSO unitDataSO, List<UnitAbility> unitAbilities, Color color)
    {
        MaxHp = unitDataSO.health;
        BaseDamage = unitDataSO.baseDamage;
        Color = color;
        UnitAbilities = unitAbilities;
    }
}

[Serializable]
public class UnitState
{
    public bool IsAlive { get; private set; } = true;
    public int CurrentHp { get; private set; }
    public List<ActionEffect> ActiveActionEffects { get; private set; }

    public UnitState(int currentHp)
    {
        CurrentHp = currentHp;
        ActiveActionEffects = new List<ActionEffect>();
    }

    public void AddActionEffect(ActionEffect actionEffect)
    {
        ActiveActionEffects.Add(actionEffect);
    }

    public void ApplyDamage(int value)
    {
        foreach(var effect in ActiveActionEffects)
        {
            if(effect is ShieldEffect)
            {
                return;
            }
        }
        CurrentHp -= value;

        if(CurrentHp <= 0)
        {
            CurrentHp = 0;
            IsAlive = false;
        }
    }

    public void ApplyHeal(int value)
    {
        CurrentHp += value;
        if(CurrentHp > 100)
        {
            CurrentHp = 100;
        }
    }

    public bool IsTaunted(out TauntEffect tauntEffect)
    {
        tauntEffect = null;

        foreach (var effect in ActiveActionEffects)
        {
            if (effect is TauntEffect)
            {
                tauntEffect = effect as TauntEffect;
                return true;
            }
        }
        return false;
    }
}

public static class UnitFactory
{
    public static Unit GetUnit(UnitIdentifier unitIdentifier, UnitDataSO unitDataSO)
    {
        return new Unit(unitIdentifier, 
            new UnitData(unitDataSO, UnitAbilityFactory.GetUnitAbilities(unitDataSO), unitIdentifier.TeamId == 0 ? Color.green : Color.red));
    }
}

public static class UnitHelpers
{
    public static UnitRelativeOwner GetRelativeOwner(int teamId, int otherTeamId)
    {
        return teamId == otherTeamId ? UnitRelativeOwner.Self : UnitRelativeOwner.Opponent;
    }

}
