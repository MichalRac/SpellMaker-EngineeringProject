using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteOverrideController : MonoBehaviour
{
    [SerializeField] Sprite alternativeSprite;

    public void UseOverride()
    {
        GetComponent<Image>().sprite = alternativeSprite;
    }
}
