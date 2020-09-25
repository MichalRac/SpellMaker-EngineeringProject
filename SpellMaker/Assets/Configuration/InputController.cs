// GENERATED AUTOMATICALLY FROM 'Assets/Configuration/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""TargetPointer"",
            ""id"": ""94de76ac-8560-4b24-b142-d2f73ebb7f89"",
            ""actions"": [
                {
                    ""name"": ""Any"",
                    ""type"": ""PassThrough"",
                    ""id"": ""220b338e-27e4-4282-a40e-4d5730f26c02"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Execute"",
                    ""type"": ""Button"",
                    ""id"": ""b08fa8a2-114a-450a-9981-b06c0ae30ad0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Movement"",
                    ""id"": ""9b8a95d9-2526-40f4-84d8-33d3d74566cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""81ee499b-863b-4662-9b32-51d277d7f87b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""71be3405-e868-4bce-8545-f47c4925e8f5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""952f4e60-1e7b-434e-8fe0-818d867d9910"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""86f080f0-3529-4ade-8148-df2e28273d3b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8cbb1380-ddb9-48a8-ab42-e17122c2b750"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Execute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraController"",
            ""id"": ""cf704e3b-980d-4d15-b10e-78165ef7b93e"",
            ""actions"": [
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Button"",
                    ""id"": ""846b039b-fec1-41c4-a7ba-f9681260b111"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""384e99b4-26c5-483c-8399-198cf146376e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5e2d33aa-da3e-441c-9828-869047155938"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c8a09656-d6ba-45ae-bf5e-3a7026320496"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TargetPointer
        m_TargetPointer = asset.FindActionMap("TargetPointer", throwIfNotFound: true);
        m_TargetPointer_Any = m_TargetPointer.FindAction("Any", throwIfNotFound: true);
        m_TargetPointer_Execute = m_TargetPointer.FindAction("Execute", throwIfNotFound: true);
        // CameraController
        m_CameraController = asset.FindActionMap("CameraController", throwIfNotFound: true);
        m_CameraController_Rotation = m_CameraController.FindAction("Rotation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // TargetPointer
    private readonly InputActionMap m_TargetPointer;
    private ITargetPointerActions m_TargetPointerActionsCallbackInterface;
    private readonly InputAction m_TargetPointer_Any;
    private readonly InputAction m_TargetPointer_Execute;
    public struct TargetPointerActions
    {
        private @InputController m_Wrapper;
        public TargetPointerActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Any => m_Wrapper.m_TargetPointer_Any;
        public InputAction @Execute => m_Wrapper.m_TargetPointer_Execute;
        public InputActionMap Get() { return m_Wrapper.m_TargetPointer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TargetPointerActions set) { return set.Get(); }
        public void SetCallbacks(ITargetPointerActions instance)
        {
            if (m_Wrapper.m_TargetPointerActionsCallbackInterface != null)
            {
                @Any.started -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnAny;
                @Any.performed -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnAny;
                @Any.canceled -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnAny;
                @Execute.started -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnExecute;
                @Execute.performed -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnExecute;
                @Execute.canceled -= m_Wrapper.m_TargetPointerActionsCallbackInterface.OnExecute;
            }
            m_Wrapper.m_TargetPointerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Any.started += instance.OnAny;
                @Any.performed += instance.OnAny;
                @Any.canceled += instance.OnAny;
                @Execute.started += instance.OnExecute;
                @Execute.performed += instance.OnExecute;
                @Execute.canceled += instance.OnExecute;
            }
        }
    }
    public TargetPointerActions @TargetPointer => new TargetPointerActions(this);

    // CameraController
    private readonly InputActionMap m_CameraController;
    private ICameraControllerActions m_CameraControllerActionsCallbackInterface;
    private readonly InputAction m_CameraController_Rotation;
    public struct CameraControllerActions
    {
        private @InputController m_Wrapper;
        public CameraControllerActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotation => m_Wrapper.m_CameraController_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_CameraController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControllerActions set) { return set.Get(); }
        public void SetCallbacks(ICameraControllerActions instance)
        {
            if (m_Wrapper.m_CameraControllerActionsCallbackInterface != null)
            {
                @Rotation.started -= m_Wrapper.m_CameraControllerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_CameraControllerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_CameraControllerActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_CameraControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public CameraControllerActions @CameraController => new CameraControllerActions(this);
    public interface ITargetPointerActions
    {
        void OnAny(InputAction.CallbackContext context);
        void OnExecute(InputAction.CallbackContext context);
    }
    public interface ICameraControllerActions
    {
        void OnRotation(InputAction.CallbackContext context);
    }
}
