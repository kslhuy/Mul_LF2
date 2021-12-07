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

        [SerializeField]
        private CharacterSwap m_CharacterSwapper;

        [SerializeField]
        private VisualizationConfiguration m_VisualizationConfiguration;
        

        /// <summary>
        /// Returns a reference to the active Animator for this visualization
        /// </summary>
        public Animator OurAnimator { get { return m_ClientVisualsAnimator; } }

        public bool CanPerformActions { get { return m_NetState.CanPerformActions; } }


        private NetworkCharacterState m_NetState;

        public Transform Parent { get; private set; }

        private PlayerStateFX m_statePlayerViz;


        /// Player characters need to report health changes and chracter info to the PartyHUD
        PartyHUD m_PartyHUD;

        float m_SmoothedSpeed;

        int m_HitStateTriggerID;
        private ClientInputSender inputSender;
        private float m_MaxDistance = 0.2f;

        event Action Destroyed;

        /// <inheritdoc />
        public void Start()
        {
            if (!NetworkManager.Singleton.IsClient || transform.parent == null)
            {
                enabled = false;
                return;
            }

            // m_HitStateTriggerID = Animator.StringToHash(ActionFX.k_DefaultHitReact);



            m_NetState = GetComponentInParent<NetworkCharacterState>();
            Parent = m_NetState.transform;


            m_NetState.DoActionEventClient += PerformActionFX;
            m_NetState.CancelAllActionsEventClient += CancelAllActionFXs;
            m_NetState.CancelActionsByTypeEventClient += CancelActionFXByType;
            m_NetState.OnStopChargingUpClient += OnStoppedChargingUp;
            m_NetState.IsStealthy.OnValueChanged += OnStealthyChanged;


            m_statePlayerViz = new PlayerStateFX( this,m_NetState.CharacterType);


            // sync our visualization position & rotation to the most up to date version received from server
            // var parentMovement = m_NetState.GetComponent<INetMovement>();
            // transform.position = parentMovement.NetworkPosition.Value;
            // transform.rotation = Quaternion.Euler(0, parentMovement.NetworkRotationY.Value, 0);

            transform.SetPositionAndRotation(m_NetState.transform.position, m_NetState.transform.rotation);


   
            // ...and visualize the current char-select value that we know about
            SetAppearanceSwap();

            // sync our animator to the most up to date version received from server
            // SyncEntryAnimation(m_NetState.LifeState);

               if (!m_NetState.IsNpc)
            {
                name = "AvatarGraphics" + m_NetState.OwnerClientId;

                if (m_NetState.IsOwner)
                {
                    // ActionRequestData data = new ActionRequestData { ActionTypeEnum = ActionType.GeneralTarget };
                    // m_ActionViz.PlayAction(ref data);
                    // gameObject.AddComponent<CameraController>();

                    if (TryGetComponent(out ClientInputSender inputSender))
                    {
                        // TODO: revisit; anticipated actions would play twice on the host
                        if (!NetworkManager.Singleton.IsServer)
                        {
                            inputSender.ActionInputEvent += OnActionInput;
                        }
                        inputSender.ClientMoveEvent += OnMoveInput;
                    }
                }
            }
        }

        // Do anticipate State : Only play Animation , not change state
        private void OnActionInput(StateRequestData data)
        {
            // m_ActionViz.AnticipateAction(ref data);
            m_statePlayerViz.AnticipateState(ref data);
        }

        
        private void PerformActionFX(StateRequestData data)
        {
            // That event do actual State from Server .
            m_statePlayerViz.PerformActionFX(ref data);
        }

        // Play Animation and change state between Idle and Move State Visual
        private void OnMoveInput(Vector2 position)
        {

            m_statePlayerViz.OnMoveInput(position);
        }




        private void OnDestroy()
        {
            if (m_NetState)
            {
                m_NetState.DoActionEventClient -= PerformActionFX;
                m_NetState.CancelAllActionsEventClient -= CancelAllActionFXs;
                m_NetState.CancelActionsByTypeEventClient -= CancelActionFXByType;
                m_NetState.OnStopChargingUpClient -= OnStoppedChargingUp;
                m_NetState.IsStealthy.OnValueChanged -= OnStealthyChanged;


                if (Parent != null && Parent.TryGetComponent(out ClientInputSender sender))
                {
                    sender.ActionInputEvent -= OnActionInput;
                    sender.ClientMoveEvent -= OnMoveInput;
                }
            }

        }



        private void CancelAllActionFXs()
        {
        }

        private void CancelActionFXByType(StateType actionType)
        {
        }

        private void OnStoppedChargingUp(float finalChargeUpPercentage)
        {
        }

        // private void OnLifeStateChanged(LifeState previousValue, LifeState newValue)
        // {
        //     switch (newValue)
        //     {
        //         case LifeState.Alive:
        //             m_ClientVisualsAnimator.SetTrigger(m_VisualizationConfiguration.AliveStateTriggerID);
        //             break;
        //         case LifeState.Fainted:
        //             m_ClientVisualsAnimator.SetTrigger(m_VisualizationConfiguration.FaintedStateTriggerID);
        //             break;
        //         case LifeState.Dead:
        //             m_ClientVisualsAnimator.SetTrigger(m_VisualizationConfiguration.DeadStateTriggerID);
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(newValue), newValue, null);
        //     }
        // }

        // private void OnHealthChanged(int previousValue, int newValue)
        // {
        //     // don't do anything if party HUD goes away - can happen as Dungeon scene is destroyed
        //     if (m_PartyHUD == null) { return; }

        //     if (IsLocalPlayer)
        //     {
        //         this.m_PartyHUD.SetHeroHealth(newValue);
        //     }
        //     // else
        //     // {
        //     //     this.m_PartyHUD.SetAllyHealth(m_NetState.NetworkObjectId, newValue);
        //     // }
        // }

        // private void OnCharacterAppearanceChanged(int oldValue, int newValue)
        // {
        //     SetAppearanceSwap();
        // }

        private void OnStealthyChanged(bool oldValue, bool newValue)
        {
            SetAppearanceSwap();
        }

        private void SetAppearanceSwap()
        {
            // if (m_CharacterSwapper)
            // {

            //     m_CharacterSwapper.SwapToModel(m_NetState.CharacterAppearance.Value);
            // }
        }



        void Update()
        {
            m_statePlayerViz.Update();
        }

        // Huy : Not use Yet        
        public void OnAnimEvent(string id)
        {
            //if you are trying to figure out who calls this method, it's "magic". The Unity Animation Event system takes method names as strings,
            //and calls a method of the same name on a component on the same GameObject as the Animator. See the "attack1" Animation Clip as one
            //example of where this is configured.

            m_statePlayerViz.OnAnimEvent(id);
        }

    }
}
