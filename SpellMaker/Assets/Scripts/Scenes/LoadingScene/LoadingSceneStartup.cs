using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadingSceneStartup : SceneStartup<LoadingSceneStartup, LoadingSceneArgs>
{
    [SerializeField] private TextMeshProUGUI loadingProgressTest;

    protected override void OnAwake()
    {
        StartCoroutine(UpdateProgress());
    }

    private IEnumerator UpdateProgress()
    {
        while (loadingProgressTest.isActiveAndEnabled)
        {
            yield return null;
            loadingProgressTest.text = $"Loading: {SceneStartupManager.nextSceneLoadProgress * 100}%";
        }
    }
}
