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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TargetPointer
        m_TargetPointer = asset.FindActionMap("TargetPointer", throwIfNotFound: true);
        m_TargetPointer_Any = m_TargetPointer.FindAction("Any", throwIfNotFound: true);
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
    public struct TargetPointerActions
    {
        private @InputController m_Wrapper;
        public TargetPointerActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Any => m_Wrapper.m_TargetPointer_Any;
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
            }
            m_Wrapper.m_TargetPointerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Any.started += instance.OnAny;
                @Any.performed += instance.OnAny;
                @Any.canceled += instance.OnAny;
            }
        }
    }
    public TargetPointerActions @TargetPointer => new TargetPointerActions(this);
    public interface ITargetPointerActions
    {
        void OnAny(InputAction.CallbackContext context);
    }
}
