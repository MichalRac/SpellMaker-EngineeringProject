using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private InputController inputController;

    // Start is called before the first frame update
    void Awake()
    {
        inputController = new InputController();
    }

    private void OnEnable()
    {
        inputController.CameraController.Enable();
    }

    private void OnDisable()
    {
        inputController.CameraController.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var cameraRotation = inputController.CameraController.Rotation.ReadValue<float>();

        if(Mathf.Abs(cameraRotation) >= float.Epsilon)
        {
            transform.Rotate(new Vector3(0f, cameraRotation * speed * Time.deltaTime, 0f));
        }
    }

    public void SwapSide(int teamId)
    {
        var newRotation = transform.rotation;
        newRotation.y = teamId == 0 ? 0f : 180f;
        transform.rotation = newRotation;
    }
}
