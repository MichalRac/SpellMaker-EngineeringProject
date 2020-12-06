using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SceneStartupManager
{
    private static readonly Dictionary<Type, SceneArgs> args;
    
    static SceneStartupManager()
    {
        args = new Dictionary<Type, SceneArgs>();
    }

    private static T GetAttribute<T>(Type type) where T : Attribute
    {
        object[] attributes = type.GetCustomAttributes(true);

        foreach (object attribute in attributes)
            if (attribute is T targetAttribute)
                return targetAttribute;

        return null;
    }

    public static AsyncOperation OpenSceneWithArgs<TController, TArgs>(TArgs sceneArgs = null)
        where TController : SceneStartup<TController, TArgs>
        where TArgs : SceneArgs, new()
    {
        Type type = typeof(TController);
        SceneStartupAttribute attribute = GetAttribute<SceneStartupAttribute>(type);

        if (attribute == null)
            throw new NullReferenceException($"You're trying to load scene startup without ${nameof(SceneStartupAttribute)}");

        string sceneName = attribute.SceneName;

        if (sceneArgs == null)
            args.Add(type, new TArgs { IsNull = true });
        else
            args.Add(type, sceneArgs);

        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public static TArgs GetArgs<TController, TArgs>()
        where TController : SceneStartup<TController, TArgs>
        where TArgs : SceneArgs, new()
    {
        Type type = typeof(TController);
        
        if(!args.ContainsKey(type) || args[type] == null)
            return new TArgs { IsNull = true };

        TArgs sceneArgs = (TArgs)args[type];

        args.Remove(type);

        return sceneArgs;
    }
}
