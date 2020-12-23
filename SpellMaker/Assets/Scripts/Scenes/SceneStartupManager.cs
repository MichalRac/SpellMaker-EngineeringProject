using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public static class SceneStartupManager
{
    private static readonly Dictionary<Type, SceneArgs> args;
    private const string LOADING_SCENE_NAME = "LoadingScene";
    private static string nextActiveScene;
    
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

        nextActiveScene = attribute.SceneName;

        if (sceneArgs == null)
            args.Add(type, new TArgs { IsNull = true });
        else
            args.Add(type, sceneArgs);

        SceneManager.LoadSceneAsync(LOADING_SCENE_NAME);

        var sceneToLoad = SceneManager.LoadSceneAsync(nextActiveScene, LoadSceneMode.Additive);
        sceneToLoad.completed += SceneToLoad_completed;

        return sceneToLoad;
    }

    private static void SceneToLoad_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextActiveScene));
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

    public static void UnloadLoadingScene()
    {
        SceneManager.UnloadSceneAsync(LOADING_SCENE_NAME);
    }
}
