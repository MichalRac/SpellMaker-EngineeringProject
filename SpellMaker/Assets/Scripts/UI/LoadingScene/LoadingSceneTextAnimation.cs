using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingSceneTextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    private int count = 0;

    public IEnumerator Start()
    {
        string suffix;
        
        while(gameObject.activeInHierarchy)
        {
            switch (count++)
            {
                default:
                case 0:
                    suffix = string.Empty;
                    break;
                case 1:
                    suffix = ".";
                    break;
                case 2:
                    suffix = "..";
                    break;
                case 3:
                    suffix = "...";
                    count = 0;
                    break;
            }

            loadingText.text = "Loading" + suffix;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
