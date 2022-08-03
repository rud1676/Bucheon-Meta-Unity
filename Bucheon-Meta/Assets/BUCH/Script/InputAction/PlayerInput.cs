//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/BUCH/Script/InputAction/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMain"",
            ""id"": ""913bf626-18a6-43f0-9690-9327ea1c78d1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0137a220-d852-4b4c-8330-d59d2cde8b5e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bdb8997d-81ed-41d2-90ce-b5b1c3cff0b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""1f74dbc1-fabe-42bd-b3f4-584cd1792d1b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""3afbac85-cabf-4892-9b0e-da4d27562f90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TearOff"",
                    ""type"": ""Button"",
                    ""id"": ""7a12dfdf-03c2-4d94-b2f4-26528163efd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Call"",
                    ""type"": ""Button"",
                    ""id"": ""df9c01b0-c020-44d8-9946-b3acd76705fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""507988ce-372a-4f32-b230-08288ef4f5a2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""870c0c59-e33c-472f-9b51-a66a91e2cf57"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bcecdcd-2e80-482c-9952-90e1cfb64273"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd12d86b-2c36-44c8-a0d9-2620d0ed5639"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8e3b027-948d-4ce9-a425-08acbe31b6a9"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ad4a980-ae0e-4ab6-8d01-0faad7382b22"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TearOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf4326d0-cebb-431e-a2a7-85ff56b3955a"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Call"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMain
        m_PlayerMain = asset.FindActionMap("PlayerMain", throwIfNotFound: true);
        m_PlayerMain_Move = m_PlayerMain.FindAction("Move", throwIfNotFound: true);
        m_PlayerMain_Jump = m_PlayerMain.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMain_Look = m_PlayerMain.FindAction("Look", throwIfNotFound: true);
        m_PlayerMain_PickUp = m_PlayerMain.FindAction("PickUp", throwIfNotFound: true);
        m_PlayerMain_TearOff = m_PlayerMain.FindAction("TearOff", throwIfNotFound: true);
        m_PlayerMain_Call = m_PlayerMain.FindAction("Call", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMain
    private readonly InputActionMap m_PlayerMain;
    private IPlayerMainActions m_PlayerMainActionsCallbackInterface;
    private readonly InputAction m_PlayerMain_Move;
    private readonly InputAction m_PlayerMain_Jump;
    private readonly InputAction m_PlayerMain_Look;
    private readonly InputAction m_PlayerMain_PickUp;
    private readonly InputAction m_PlayerMain_TearOff;
    private readonly InputAction m_PlayerMain_Call;
    public struct PlayerMainActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMainActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMain_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerMain_Jump;
        public InputAction @Look => m_Wrapper.m_PlayerMain_Look;
        public InputAction @PickUp => m_Wrapper.m_PlayerMain_PickUp;
        public InputAction @TearOff => m_Wrapper.m_PlayerMain_TearOff;
        public InputAction @Call => m_Wrapper.m_PlayerMain_Call;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMainActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMainActions instance)
        {
            if (m_Wrapper.m_PlayerMainActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
                @PickUp.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnPickUp;
                @TearOff.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnTearOff;
                @TearOff.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnTearOff;
                @TearOff.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnTearOff;
                @Call.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnCall;
                @Call.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnCall;
                @Call.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnCall;
            }
            m_Wrapper.m_PlayerMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @TearOff.started += instance.OnTearOff;
                @TearOff.performed += instance.OnTearOff;
                @TearOff.canceled += instance.OnTearOff;
                @Call.started += instance.OnCall;
                @Call.performed += instance.OnCall;
                @Call.canceled += instance.OnCall;
            }
        }
    }
    public PlayerMainActions @PlayerMain => new PlayerMainActions(this);
    public interface IPlayerMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnTearOff(InputAction.CallbackContext context);
        void OnCall(InputAction.CallbackContext context);
    }
}