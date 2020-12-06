using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SceneStartupAttribute : Attribute
{
    public string SceneName { get; private set; }

    public SceneStartupAttribute(string name)
    {
        SceneName = name;
    }
}
