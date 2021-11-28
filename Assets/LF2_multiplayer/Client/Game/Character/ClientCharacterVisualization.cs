using System.Collections.Generic;
using LF2.Client;
// using Cinemachine;
using MLAPI;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Visual
{
    /// <summary>
    /// <see cref="ClientCharacterVisualization"/> is responsible for displaying a character on the client's screen based on state information sent by the server.
    /// </summary>
    public class ClientCharacterVisualization : NetworkBehaviour
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

        private PlayerStateFX m_statePlayerViz;


        /// Player characters need to report health changes and chracter info to the PartyHUD
        PartyHUD m_PartyHUD;

        float m_SmoothedSpeed;

        int m_HitStateTriggerID;
        private ClientInputSender inputSender;
        private float m_MaxDistance = 0.2f;

        event Action Destroyed;

        /// <inheritdoc />
        public override void NetworkStart()
        {
            if (!IsClient || transform.parent == null)
            {
                enabled = false;
                return;
            }

            // m_HitStateTriggerID = Animator.StringToHash(ActionFX.k_DefaultHitReact);


            // Parent = transform.parent;

            m_NetState = GetComponentInParent<NetworkCharacterState>();
            m_NetState.DoActionEventClient += PerformActionFX;
            m_NetState.CancelAllActionsEventClient += CancelAllActionFXs;
            m_NetState.CancelActionsByTypeEventClient += CancelActionFXByType;
            // m_NetState.NetworkLifeState.LifeState.OnValueChanged += OnLifeStateChanged;
            m_NetState.OnPerformHitReaction += OnPerformHitReaction;
            m_NetState.OnStopChargingUpClient += OnStoppedChargingUp;
            m_NetState.IsStealthy.OnValueChanged += OnStealthyChanged;
            // Debug.Log(m_NetState.CharacterType);

            m_statePlayerViz = new PlayerStateFX( this,m_NetState.CharacterType);


            // sync our visualization position & rotation to the most up to date version received from server
            // var parentMovement = m_NetState.GetComponent<INetMovement>();
            // transform.position = parentMovement.NetworkPosition.Value;
            // transform.rotation = Quaternion.Euler(0, parentMovement.NetworkRotationY.Value, 0);

            transform.SetPositionAndRotation(m_NetState.transform.position, m_NetState.transform.rotation);


            // listen for char-select info to change (in practice, this info doesn't
            // change, but we may not have the values set yet) ...
            m_NetState.CharacterAppearance.OnValueChanged += OnCharacterAppearanceChanged;

            // ...and visualize the current char-select value that we know about
            OnCharacterAppearanceChanged(0, m_NetState.CharacterAppearance.Value);

            // ...and visualize the current char-select value that we know about
            SetAppearanceSwap();

            // sync our animator to the most up to date version received from server
            // SyncEntryAnimation(m_NetState.LifeState);

            if (!m_NetState.IsNpc)
            {
                // track health for heroes
                m_NetState.HealthState.HitPoints.OnValueChanged += OnHealthChanged;

                // find the emote bar to track its buttons
                GameObject partyHUDobj = GameObject.FindGameObjectWithTag("PartyHUD");
                m_PartyHUD = partyHUDobj.GetComponent<Visual.PartyHUD>();

                if (IsLocalPlayer)
                {
                    // gameObject.AddComponent<CameraController>();
                    
                    // // UI 
                    m_PartyHUD.SetHeroData(m_NetState);

                    inputSender = GetComponentInParent<ClientInputSender>(); 
                    inputSender.ActionInputEvent += OnActionInput;
                    inputSender.ClientMoveEvent += OnMoveInput;
                }
                else
                {
                    // m_PartyHUD.SetAllyData(m_NetState);

                    // getting our parent's NetworkObjectID for PartyHUD removal on Destroy
                    var parentNetworkObjectID = m_NetState.NetworkObjectId;

                    // once this object is destroyed, remove this ally from the PartyHUD UI
                    // NOTE: architecturally this will be refactored
                    // Destroyed += () =>
                    // {
                    //     if (m_PartyHUD != null)
                    //     {
                    //         m_PartyHUD.RemoveAlly(parentNetworkObjectID);
                    //     }
                    // };
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
                // m_NetState.NetworkLifeState.LifeState.OnValueChanged -= OnLifeStateChanged;
                m_NetState.OnPerformHitReaction -= OnPerformHitReaction;
                m_NetState.OnStopChargingUpClient -= OnStoppedChargingUp;
                m_NetState.IsStealthy.OnValueChanged -= OnStealthyChanged;

                inputSender.ActionInputEvent -= OnActionInput;
                inputSender.ClientMoveEvent -= OnMoveInput;
            }

            Destroyed?.Invoke();
        }

        private void OnPerformHitReaction()
        {
            m_ClientVisualsAnimator.SetTrigger(m_HitStateTriggerID);
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

        private void OnHealthChanged(int previousValue, int newValue)
        {
            // don't do anything if party HUD goes away - can happen as Dungeon scene is destroyed
            if (m_PartyHUD == null) { return; }

            if (IsLocalPlayer)
            {
                this.m_PartyHUD.SetHeroHealth(newValue);
            }
            // else
            // {
            //     this.m_PartyHUD.SetAllyHealth(m_NetState.NetworkObjectId, newValue);
            // }
        }

        private void OnCharacterAppearanceChanged(int oldValue, int newValue)
        {
            SetAppearanceSwap();
        }

        private void OnStealthyChanged(bool oldValue, bool newValue)
        {
            SetAppearanceSwap();
        }

        private void SetAppearanceSwap()
        {
            if (m_CharacterSwapper)
            {

                m_CharacterSwapper.SwapToModel(m_NetState.CharacterAppearance.Value);
            }
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
