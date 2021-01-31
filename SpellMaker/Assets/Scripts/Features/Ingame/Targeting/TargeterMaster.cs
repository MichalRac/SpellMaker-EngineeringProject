using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargeterMaster : MonoBehaviour
{
    [SerializeField] private PointTargeter basicPointTargeter;
    [SerializeField] private PointTargeter pointTargeter;
    [SerializeField] private CircleTargeter circleTargeter;
    [SerializeField] private LineTargeter lineTargeter;

    [SerializeField] private float targeterMovementSpeed = 1f;

    private ITargeter currentTargeter;
    private ITargeter additionalTargeter;

    private InputController inputController;
    private Camera mainCamera;
    private Action<TargetingResultData> targetingResultCallback;


    void Awake()
    {
        mainCamera = Camera.main;
        inputController = new InputController();
    }

    public void StartTargeting(Vector3 initPos, UnitAbility ability, Action<TargetingResultData> targetingResultCallback)
    {
        this.targetingResultCallback = targetingResultCallback;

        switch (ability.TargetingType)
        {
            case TargetingType.Single:
            case TargetingType.Self:
                currentTargeter = pointTargeter;
                additionalTargeter = null;
                break;
            case TargetingType.Line:
                currentTargeter = lineTargeter;
                additionalTargeter = basicPointTargeter;
                break;
            case TargetingType.Circle:
                currentTargeter = circleTargeter;
                additionalTargeter = basicPointTargeter;
                break;
            case TargetingType.All:
            default:
                Debug.Log($"[TargeterMaster] TargeterType unhandled {ability.TargetingType}");
                break;
        }

        
        currentTargeter.Setup(initPos, ability.AbilitySize, ability.TargetGroup);
        additionalTargeter?.Setup(initPos, AbilitySize.None, null);

        inputController.TargetPointer.Any.Enable();
        inputController.TargetPointer.Execute.Enable();

        inputController.TargetPointer.Execute.performed += Execute_performed;
    }


    public void CancelTargeting()
    {
        currentTargeter?.CancelTargeting();
        additionalTargeter?.CancelTargeting();
        inputController?.Disable();
    }

    private void Execute_performed(InputAction.CallbackContext obj)
    {
        var targets = currentTargeter.ExecuteTargeting();
        additionalTargeter?.CancelTargeting();
        targetingResultCallback?.Invoke(targets);
        inputController.Disable();
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
        additionalTargeter?.Move(deltaMovement);
    }
}
