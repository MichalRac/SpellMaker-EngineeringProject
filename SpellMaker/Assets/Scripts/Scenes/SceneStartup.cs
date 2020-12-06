using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneStartup<TController, TArgs> : MonoBehaviour
    where TController : SceneStartup<TController, TArgs>
    where TArgs : SceneArgs, new()
{
    protected TArgs Args { get; private set; }
    
    private void Awake()
    {
        Args = SceneStartupManager.GetArgs<TController, TArgs>();
        OnAwake();
    }

    protected virtual void OnAwake() { }
}
