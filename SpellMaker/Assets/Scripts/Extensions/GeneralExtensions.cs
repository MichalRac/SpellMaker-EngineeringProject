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

}
