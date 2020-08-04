// GENERATED AUTOMATICALLY FROM 'Assets/_game/Core/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Skahal.SpaceInvadersRemake
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Cannon"",
            ""id"": ""87ab527b-de3e-4c89-8554-892f47891c39"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""223cfc75-0aa8-4b3c-b7b0-9e0bae84d07d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""edfcabb8-5ee2-4dbf-981a-9cd9de19bde3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""19702a64-19bd-47d6-a2f8-f7e20158ceca"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b726f3b-4e96-48d8-b155-097dc327571b"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3a3e94f-ee07-4356-935f-061fd97e5208"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3320c59-482e-4aae-8f51-9db826c1b494"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""2adf2e9a-fd16-4b0c-b7ec-d5004cf59aaa"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bbaeb8c7-65b1-447e-a2e2-89d98b137e12"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f2d82bed-5c82-46f3-ab24-ab2e9c54e0ff"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""cff53ffd-100a-484d-983f-333bad52cbb7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c2e2ab8c-8862-40e9-ab08-3ddfa02a5e68"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7e84c984-df70-42a4-a5c7-95ac14fa2622"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Cannon
            m_Cannon = asset.FindActionMap("Cannon", throwIfNotFound: true);
            m_Cannon_Fire = m_Cannon.FindAction("Fire", throwIfNotFound: true);
            m_Cannon_Move = m_Cannon.FindAction("Move", throwIfNotFound: true);
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

        // Cannon
        private readonly InputActionMap m_Cannon;
        private ICannonActions m_CannonActionsCallbackInterface;
        private readonly InputAction m_Cannon_Fire;
        private readonly InputAction m_Cannon_Move;
        public struct CannonActions
        {
            private @Controls m_Wrapper;
            public CannonActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Fire => m_Wrapper.m_Cannon_Fire;
            public InputAction @Move => m_Wrapper.m_Cannon_Move;
            public InputActionMap Get() { return m_Wrapper.m_Cannon; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CannonActions set) { return set.Get(); }
            public void SetCallbacks(ICannonActions instance)
            {
                if (m_Wrapper.m_CannonActionsCallbackInterface != null)
                {
                    @Fire.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnFire;
                    @Move.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_CannonActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public CannonActions @Cannon => new CannonActions(this);
        public interface ICannonActions
        {
            void OnFire(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
    }
}
