using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutiner : MonoBehaviour
{
    public static Coroutiner Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public Coroutine CallWithDelay(float delayInSeconds, Action callback)
    {
        return StartCoroutine(CallWithDelayCoroutine(delayInSeconds, callback));
    }

    private IEnumerator CallWithDelayCoroutine(float delayInSeconds, Action callback)
    {
        yield return new WaitForSeconds(delayInSeconds);
        callback?.Invoke();
    }


    public Coroutine CallWithFrameDelay(Action callback)
    {
        return StartCoroutine(CallWithFrameDelayCoroutine(callback));
    }

    public IEnumerator CallWithFrameDelayCoroutine(Action callback)
    {
        yield return null;
        callback?.Invoke();
    }
}
