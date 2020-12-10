using System.Collections;
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
}
