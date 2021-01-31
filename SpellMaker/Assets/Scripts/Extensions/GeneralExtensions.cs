﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralExtensions
{
    public static void DestroyAllChildren(this Transform transform)
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private static System.Random rng = new System.Random();

    //  Returns a random number between −1 and 1, where values around zero are more likely.
    public static float RandomBinomial() => Random.value - Random.value;
    public static float RandomBinomialMultiplied(float multiplier) => RandomBinomial() * multiplier;

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Queue<T> ToQueue<T>(this IList<T> list)
    {
        Queue<T> queue = new Queue<T>();
        foreach(T item in list)
        {
            queue.Enqueue(item);
        }
        return queue;
    }

    public static UnitIdentifier GetUnitIdentifier(this BaseCharacterMaster baseCharacterMaster)
    {
        return baseCharacterMaster.Unit.unitIdentifier;
    }

    public static List<UnitIdentifier> GetUnitIdentifiers(this List<BaseCharacterMaster> baseCharacterMasters)
    {
        var identifiers = new List<UnitIdentifier>();

        foreach(var bcm in baseCharacterMasters)
        {
            identifiers.Add(bcm.Unit.unitIdentifier);
        }

        return identifiers;
    }
}
