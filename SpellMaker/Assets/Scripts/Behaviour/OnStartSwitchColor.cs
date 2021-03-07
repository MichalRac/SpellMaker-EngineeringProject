using UnityEngine;
using UnityEngine.UI;

public class OnStartSwitchColor : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color color;

    void Start()
    {
        image.color = color;
    }
}
