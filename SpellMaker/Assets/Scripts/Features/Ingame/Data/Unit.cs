﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit : ICloneable
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
    
    public void SimulateActivateActionEffects()
    {
        for(int i = UnitState.ActiveActionEffects.Count - 1; i >= 0; i--)
        {
            if(UnitState.ActiveActionEffects[i].IsFinished())
            {
                UnitState.ActiveActionEffects.RemoveAt(i);
            }
            else
            {
                UnitState.ActiveActionEffects[i].SimulateAffect(this, true);
            }
        }
    }

    private Unit(UnitIdentifier unitIdentifier, UnitData unitData, UnitState unitState)
    {
        UnitIdentifier = unitIdentifier;
        UnitData = unitData;
        UnitState = unitState;
    }

    public object Clone()
    {
        return new Unit(UnitIdentifier, (UnitData)UnitData.Clone(), (UnitState)UnitState.Clone());
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
public class UnitData : ICloneable
{
    public int MaxHp { get; private set; }
    public int BaseDamage { get; }
    public Vector3 Position { get; set; }

    public Color Color { get; }
    public List<UnitAbility> UnitAbilities { get; }
    public List<Goal> Goals { get; }

    public UnitData(UnitDataSO unitDataSO, List<UnitAbility> unitAbilities, List<Goal> goals, Color color, Vector3 initialPosition)
    {
        MaxHp = unitDataSO.health;
        BaseDamage = unitDataSO.baseDamage;
        Color = color;
        UnitAbilities = unitAbilities;
        Goals = goals;
        Position = initialPosition;
    }

    //For deep copy purposes
    private UnitData(int maxHp, int baseDamage, Vector3 position, Color color, List<UnitAbility> unitAbilities, List<Goal> goals)
    {
        MaxHp = maxHp;
        BaseDamage = baseDamage;
        Position = new Vector3(position.x, position.y, position.z);
        Color = color;
        UnitAbilities = unitAbilities;
        Goals = goals;
    }

    public object Clone()
    {
        return new UnitData(MaxHp, BaseDamage, Position, Color, UnitAbilities, Goals);
    }
}

[Serializable]
public class UnitState : ICloneable
{
    public bool IsAlive { get; private set; } = true;
    public int CurrentHp { get; private set; }
    public List<ActionEffect> ActiveActionEffects { get; private set; }

    public UnitState(int currentHp)
    {
        CurrentHp = currentHp;
        ActiveActionEffects = new List<ActionEffect>();
    }

    private UnitState(bool isAlive, int currentHp, List<ActionEffect> activeActionEffects)
    {
        IsAlive = isAlive;
        CurrentHp = currentHp;
        ActiveActionEffects = activeActionEffects;
    }

    public object Clone()
    {
        var copiedEffects = new List<ActionEffect>();
        foreach (var activeActionEffect in ActiveActionEffects)
        {
            copiedEffects.Add((ActionEffect)activeActionEffect.Clone());
        }
        return new UnitState(IsAlive, CurrentHp, copiedEffects);
    }


    public void AddActionEffect(ActionEffect actionEffect)
    {
        ActiveActionEffects.Add(actionEffect);
    }
    
    public void ApplyDamage(int value, bool ignoreShield = false)
    {
        if (!ignoreShield)
        {
            foreach(var effect in ActiveActionEffects)
            {
                if(effect is ShieldEffect)
                {
                    return;
                }
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
    public static Unit GetUnit(UnitIdentifier unitIdentifier, UnitDataSO unitDataSO, Vector3 initialPosition)
    {
        return 
            new Unit(unitIdentifier, 
                new UnitData(unitDataSO, 
                    UnitAbilityFactory.GetUnitAbilities(unitDataSO), 
                    unitDataSO.goalOrientedBehaviourPresetSO.goals, 
                    unitIdentifier.TeamId == 0 
                        ? Color.green 
                        : Color.red, 
                    initialPosition)
                );
    }
}

public static class UnitHelpers
{
    public static UnitRelativeOwner GetRelativeOwner(int teamId, int otherTeamId)
    {
        return teamId == otherTeamId ? UnitRelativeOwner.Self : UnitRelativeOwner.Opponent;
    }

    public static float GetDistanceBetweenUnits(Unit targetA, Unit targetB)
    {
        return Vector3.Distance(targetA.UnitData.Position, targetB.UnitData.Position);
    }

}
