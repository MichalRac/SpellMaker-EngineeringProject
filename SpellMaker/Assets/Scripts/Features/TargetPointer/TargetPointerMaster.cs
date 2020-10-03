using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetPointerMaster : MonoBehaviour
{
    private Camera camera;
    private InputController inputController;
    
    [SerializeField]
    private TargetPointerPresenter targetPointerPresenter;
    [SerializeField]
    private float speed = 1f;

    private void Awake()
    {
        camera = Camera.main;
        inputController = new InputController();
    }

    private void OnEnable()
    {
        inputController.TargetPointer.Any.Enable();
        inputController.TargetPointer.Execute.Enable();

        inputController.TargetPointer.Execute.performed += Execute_performed;
    }

    private void Execute_performed(InputAction.CallbackContext obj) => Execute();

    private void OnDisable()
    {
        inputController.TargetPointer.Any.Disable();
        inputController.TargetPointer.Execute.Disable();
        inputController.TargetPointer.Execute.performed -= Execute_performed;
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

    private IUnit selectedUnit;

    private void Execute()
    {
        //Send position event
        if(selectedUnit == null && highLightedUnit != null)
        {
            selectedUnit = highLightedUnit;
            selectedUnit.SetHighlight(true);
        }

        if(selectedUnit != null && highLightedUnit == null)
        {
            selectedUnit.SetHighlight(false);
            selectedUnit = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponent<IUnit>() != null)
        {

        }

    }

    private IUnit highLightedUnit;

    private void OnTriggerEnter(Collider other)
    {
        if(highLightedUnit == null)
        {
            highLightedUnit = other.GetComponent<IUnit>();
            if(highLightedUnit != null)
            {
                highLightedUnit.SetSelect(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(highLightedUnit != null)
        {
            highLightedUnit.SetSelect(false);
            highLightedUnit = null;
        }
    }
}
