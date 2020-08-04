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
                    ""name"": """",
                    ""id"": ""3b726f3b-4e96-48d8-b155-097dc327571b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2369f7f9-543c-402e-b250-6bf87119a3cd"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f535d49a-d28c-4586-bdd3-9ffe98222ce8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee937a20-baa0-4ca8-bf51-aa6853910160"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard arrows"",
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
                    ""name"": ""Keyboard WASD"",
                    ""id"": ""37beeea2-3bc4-4f4b-bcea-ded4debdceed"",
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
                    ""id"": ""da4c770e-b192-42bc-b27a-5e76dcbeabbd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a90ea3a5-deb1-4ef1-b4ca-984667187bb1"",
                    ""path"": ""<Keyboard>/d"",
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
        },
        {
            ""name"": ""Global"",
            ""id"": ""c9e37f68-0bbc-46a5-92bd-4f01cdbbbbe1"",
            ""actions"": [
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""8feead11-a070-4a7a-b8fc-78151a477210"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""767cfa91-dcd9-46f9-83d4-5650bc7e1b64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ed4594ba-9ea6-4731-af5d-b127ee475324"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d02cdeed-d34b-435a-be54-7a6eb6ec0d05"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e9a12c1-01c4-4de4-836b-9723237c9698"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
            // Global
            m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
            m_Global_Restart = m_Global.FindAction("Restart", throwIfNotFound: true);
            m_Global_Quit = m_Global.FindAction("Quit", throwIfNotFound: true);
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

        // Global
        private readonly InputActionMap m_Global;
        private IGlobalActions m_GlobalActionsCallbackInterface;
        private readonly InputAction m_Global_Restart;
        private readonly InputAction m_Global_Quit;
        public struct GlobalActions
        {
            private @Controls m_Wrapper;
            public GlobalActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Restart => m_Wrapper.m_Global_Restart;
            public InputAction @Quit => m_Wrapper.m_Global_Quit;
            public InputActionMap Get() { return m_Wrapper.m_Global; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
            public void SetCallbacks(IGlobalActions instance)
            {
                if (m_Wrapper.m_GlobalActionsCallbackInterface != null)
                {
                    @Restart.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnRestart;
                    @Restart.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnRestart;
                    @Restart.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnRestart;
                    @Quit.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
                    @Quit.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
                    @Quit.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnQuit;
                }
                m_Wrapper.m_GlobalActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Restart.started += instance.OnRestart;
                    @Restart.performed += instance.OnRestart;
                    @Restart.canceled += instance.OnRestart;
                    @Quit.started += instance.OnQuit;
                    @Quit.performed += instance.OnQuit;
                    @Quit.canceled += instance.OnQuit;
                }
            }
        }
        public GlobalActions @Global => new GlobalActions(this);
        public interface ICannonActions
        {
            void OnFire(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IGlobalActions
        {
            void OnRestart(InputAction.CallbackContext context);
            void OnQuit(InputAction.CallbackContext context);
        }
    }
}
