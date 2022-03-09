using System.Collections.Generic;
using LF2.Client;
// using Cinemachine;
using Unity.Netcode;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Visual
{
    /// <summary>
    /// <see cref="ClientCharacterVisualization"/> is responsible for displaying a character on the client's screen based on state information sent by the server.
    /// </summary>
    public class ClientCharacterVisualization : MonoBehaviour
    {
        [SerializeField]
        private Animator m_ClientVisualsAnimator;
        CharacterSwap m_CharacterSwapper;


        private ClientInputSender inputSender;
        [SerializeField]
        private VisualizationConfiguration m_VisualizationConfiguration;
        

        /// <summary>
        /// Returns a reference to the active Animator for this visualization
        /// </summary>
        public Animator OurAnimator { get { return m_ClientVisualsAnimator; } }

        public bool CanPerformActions { get { return m_NetState.CanPerformActions; } }

        public Transform Parent { get; private set; }

        PhysicsWrapper m_PhysicsWrapper;

        public PhysicsWrapper PhysicsWrapper => m_PhysicsWrapper ;

        [SerializeField]
        public ClientCoreMovement coreMovement  ;


        public NetworkCharacterState m_NetState;

        public ulong NetworkObjectId => m_NetState.NetworkObjectId;



        public PlayerStateMachineFX MStateMachinePlayerViz{ get; private set; } 


        /// Player characters need to report health changes and chracter info to the PartyHUD
        PartyHUD m_PartyHUD;



        /// <inheritdoc />
        public void Start()
        {            
            if (!NetworkManager.Singleton.IsClient || transform.parent == null)
            {
                enabled = false;
                return;
            }

            m_NetState = GetComponentInParent<NetworkCharacterState>();

            Parent = m_NetState.transform;



            m_PhysicsWrapper = m_NetState.GetComponent<PhysicsWrapper>();

            MStateMachinePlayerViz = new PlayerStateMachineFX( this,m_NetState.CharacterType);


            m_NetState.DoActionEventClient += PerformActionFX;
    

            // sync our visualization position & rotation to the most up to date version received from server

            transform.SetPositionAndRotation(m_PhysicsWrapper.Transform.position, m_PhysicsWrapper.Transform.rotation);


            if (!m_NetState.IsNpc)
            {
                name = "AvatarGraphics" + m_NetState.OwnerClientId;
                if (Parent.TryGetComponent(out ClientAvatarGuidHandler clientAvatarGuidHandler))
                {
                    m_ClientVisualsAnimator = clientAvatarGuidHandler.graphicsAnimator;
                }

                m_CharacterSwapper = GetComponentInChildren<CharacterSwap>();

                // ...and visualize the current char-select value that we know about
                SetAppearanceSwap();

                if (m_NetState.IsOwner)
                {
                    gameObject.AddComponent<CameraController>();
                    inputSender = GetComponentInParent<ClientInputSender>();
                    // Debug.Log(inputSender);
                    // TODO: revisit; anticipated actions would play twice on the host
                    
                    inputSender.ActionInputEvent += OnActionInput;
                    inputSender.ClientMoveEvent += OnMoveInput;
                
                }
            }
        }

        // HUY extend late :  Co the su dung lam chieu tang' hinh' cua Rudolf 
        void SetAppearanceSwap()
        {
            m_CharacterSwapper.SwapToModel();
        }

        // Do anticipate State : Only play Animation , not change state
        private void OnActionInput(StateRequestData data)
        {
            MStateMachinePlayerViz.AnticipateState(ref data);
        }

        
        private void PerformActionFX(StateRequestData data)
        {
            // That event do actual State from Server .
            MStateMachinePlayerViz.PerformActionFX(ref data);
        }

        // Play Animation and change state between Idle and Move State Visual
        private void OnMoveInput(Vector2 position)
        {
            
            MStateMachinePlayerViz.OnMoveInput(position);
        }

        private void OnDestroy()
        {
            if (m_NetState)
            {
                m_NetState.DoActionEventClient -= PerformActionFX;
  
                if (m_NetState.IsOwner)
                {
                    
                        inputSender.ActionInputEvent -= OnActionInput;
                        inputSender.ClientMoveEvent -= OnMoveInput;

                }
                
            }

        }





        void Update()
        {
            MStateMachinePlayerViz.Update();
        }

        // Huy : Not use Yet        
        public void OnAnimEvent(string id)
        {
            //if you are trying to figure out who calls this method, it's "magic". The Unity Animation Event system takes method names as strings,
            //and calls a method of the same name on a component on the same GameObject as the Animator. See the "attack1" Animation Clip as one
            //example of where this is configured.

            MStateMachinePlayerViz.OnAnimEvent(id);
        }

        /// <summary>
        /// Returns the value we should set the Animator's "Speed" variable, given current gameplay conditions.
        /// </summary>
        private float GetVisualMovementSpeed()
        {
            switch (m_NetState.MovementStatus.Value)
            {
                case MovementStatus.Walking:
                    return 1;
                default:
                    return 0;
            }
        }

        public void OnGameplayActivityVisual(ref StateRequestData stateRequestData){
            MStateMachinePlayerViz.OnGameplayActivityVisual(ref stateRequestData);
        }

        private void OnTriggerEnter(Collider other) {
            MStateMachinePlayerViz.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other) {
            MStateMachinePlayerViz.OnTriggerExit(other);
        }
    }
}
