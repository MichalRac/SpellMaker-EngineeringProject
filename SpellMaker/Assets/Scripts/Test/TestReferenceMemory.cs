using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReferenceMemory : MonoBehaviour
{
    [SerializeField] List<MatchPreparationPresenter> presenters;

    public void OnSingleActivated()
    {
        Instantiate(presenters[0]);

    }

    public void OnAllActivated()
    {
        foreach(var presenter in presenters)
        {
            Instantiate(presenter);
        }
    }
}
