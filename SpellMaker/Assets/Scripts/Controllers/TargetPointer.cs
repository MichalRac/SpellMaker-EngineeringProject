using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetPointer : MonoBehaviour
{
    private Camera camera;
    private InputController inputController;

    [SerializeField]
    private float speed = 1f;
    private Vector3 delta;

    private void Awake()
    {
        camera = Camera.main;
        inputController = new InputController();
    }

    private void OnEnable()
    {
        inputController.TargetPointer.Any.Enable();
    }

    private void OnDisable()
    {
        inputController.TargetPointer.Any.Disable();
    }

    private void FixedUpdate()
    {

        Vector2 movement = inputController.TargetPointer.Any.ReadValue<Vector2>();

        if (Mathf.Abs(movement.x) >= float.Epsilon)
        {
            Move(camera.transform.right * movement.x);
        }

        if (Mathf.Abs(movement.y) >= float.Epsilon)
        {
            Move(camera.transform.forward * movement.y);
        }
    }

    private void Move(Vector3 direction)
    {
        direction.y = 0f;

        var deltaMovement = transform.position + direction * speed * Time.deltaTime;

        transform.position = deltaMovement;
    }

}
