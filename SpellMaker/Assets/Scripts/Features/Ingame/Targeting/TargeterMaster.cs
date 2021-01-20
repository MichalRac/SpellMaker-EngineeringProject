using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargeterMaster : MonoBehaviour
{
    [SerializeField] private AreaTargeter pointTargeter;
    [SerializeField] private AreaTargeter circleTargeter;
    [SerializeField] private AreaTargeter lineTargeter;
    [SerializeField] private float targeterMovementSpeed = 1f;

    private ITargeter currentTargeter;
    private InputController inputController;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputController = new InputController();
    }

    public void StartTargeting(AbilitySetupSO abilitySetupSO)
    {

        switch (abilitySetupSO.TargetingType)
        {
            case TargetingType.Single:
            case TargetingType.Self:
                currentTargeter = pointTargeter;
                break;
            case TargetingType.Line:
                currentTargeter = lineTargeter;
                break;
            case TargetingType.Circle:
                currentTargeter = circleTargeter;
                break;
            case TargetingType.All:
            default:
                Debug.Log($"[TargeterMaster] TargeterType unhandled {abilitySetupSO.TargetingType}");
                break;
        }

        inputController.TargetPointer.Any.Enable();
        inputController.TargetPointer.Execute.Enable();

        inputController.TargetPointer.Execute.performed += Execute_performed;
    }

    private void Execute_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        inputController.TargetPointer.Any.Disable();
        inputController.TargetPointer.Execute.Disable();
        inputController.TargetPointer.Execute.performed -= Execute_performed;
    }

    private void FixedUpdate()
    {
        if (!inputController.TargetPointer.enabled)
            return;

        Vector2 movement = inputController.TargetPointer.Any.ReadValue<Vector2>();

        if (Mathf.Abs(movement.x) >= float.Epsilon)
        {
            Move(mainCamera.transform.right * movement.x);
        }

        if (Mathf.Abs(movement.y) >= float.Epsilon)
        {
            Move(mainCamera.transform.forward * movement.y);
        }
    }

    private void Move(Vector3 direction)
    {
        direction.y = 0f;

        var deltaMovement = transform.position + direction * targeterMovementSpeed * Time.deltaTime;

        currentTargeter.Move(deltaMovement);
    }
}
