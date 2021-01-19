using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEffectFactory
{
    public static ActionEffect GetActionEffect(ActionEffectData actionEffectData, BaseCharacterMaster optionalTarget = null)
    {
        switch (actionEffectData.AbilityEffectType)
        {
            case ActionEffectType.Damage:
                return new DamageEffect(actionEffectData);
            case ActionEffectType.Heal:
                return new HealEffect(actionEffectData);
            case ActionEffectType.Taunt:
                return new TauntEffect(actionEffectData, optionalTarget);
            case ActionEffectType.Shield:
                return new ShieldEffect(actionEffectData);
            default:
                Debug.Log("[ActionEffectFactory] ActionEffectType not defined in the ActionEffectFactory");
                return new DamageEffect(0, 0);
        }
    }
}
