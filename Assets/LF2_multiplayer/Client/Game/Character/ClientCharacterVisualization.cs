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
        
        // OLD MLAPI ****

        // [SerializeField]
        // TransformVariable m_RuntimeObjectsParent;

        /// <summary>
        /// Returns a reference to the active Animator for this visualization
        /// </summary>
        public Animator OurAnimator { get { return m_ClientVisualsAnimator; } }

        /// <summary>
        /// Returns the targeting-reticule prefab for this character visualization
        /// </summary>
        // public GameObject TargetReticulePrefab { get { return m_VisualizationConfiguration.TargetReticule; } }

        /// <summary>
        /// Returns the Material to plug into the reticule when the selected entity is hostile
        /// </summary>
        // public Material ReticuleHostileMat { get { return m_VisualizationConfiguration.ReticuleHostileMat; } }

        /// <summary>
        /// Returns the Material to plug into the reticule when the selected entity is friendly
        /// </summary>
        // public Material ReticuleFriendlyMat { get { return m_VisualizationConfiguration.ReticuleFriendlyMat; } }

        // /// <summary>
        // /// Returns our pseudo-Parent, the object that owns the visualization.
        // /// (We don't have an actual transform parent because we're on a top-level GameObject.)
        // /// </summary>
        // public Transform Parent { get; private set; }

        public bool CanPerformActions { get { return m_NetState.CanPerformActions; } }


        private NetworkCharacterState m_NetState;

        private PlayerStateFX m_statePlayerViz;

        private const float k_MaxRotSpeed = 280;  //max angular speed at which we will rotate, in degrees/second.

        /// Player characters need to report health changes and chracter info to the PartyHUD
        // PartyHUD m_PartyHUD;

        float m_SmoothedSpeed;

        int m_HitStateTriggerID;
        private ClientInputSender inputSender;

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
                // m_NetState.HealthState.HitPoints.OnValueChanged += OnHealthChanged;

                // find the emote bar to track its buttons
                // GameObject partyHUDobj = GameObject.FindGameObjectWithTag("PartyHUD");
                // m_PartyHUD = partyHUDobj.GetComponent<Visual.PartyHUD>();

                if (IsLocalPlayer)
                {
                    //// ko co y nghia
                    // ActionRequestData data = new ActionRequestData { ActionTypeEnum = ActionType.GeneralTarget };
                    // m_ActionViz.PlayAction(ref data);



                    // gameObject.AddComponent<CameraController>();
                    
                    // // UI 
                    // m_PartyHUD.SetHeroData(m_NetState);

                    // if (Parent.TryGetComponent(out ClientInputSender inputSender))
                    // {
                    //     inputSender.ActionInputEvent += OnActionInput;
                    //     inputSender.ClientMoveEvent += OnMoveInput;
                    // }
                    // TryGetComponent(out ClientInputSender );
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

        private void OnActionInput(ActionRequestData data)
        {
            // m_ActionViz.AnticipateAction(ref data);
            m_statePlayerViz.AnticipateState(ref data);
        }

        
        private void PerformActionFX(ActionRequestData data)
        {
            // That event do actual State from Server .
            // m_ActionViz.PlayAction(ref data);
            m_statePlayerViz.PlayState(ref data);
        }


        private void OnMoveInput(Vector2 position)
        {
            // if (m_NetState.MovementStatus.Value == MovementStatus.Idle && position != Vector2.zero){
            //             // OurAnimator.Play(m_VisualizationConfiguration.AnticipateMoveTriggerID);
            //     OurAnimator.Play("Walk_anim");
            // }
            // else if (m_NetState.MovementStatus.Value == MovementStatus.Move && position == Vector2.zero){
            //     OurAnimator.Play("Idle_anim");
            // }
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
            // m_ActionViz.CancelAllActions();
        }

        private void CancelActionFXByType(ActionType actionType)
        {
            // m_ActionViz.CancelAllActionsOfType(actionType);
        }

        private void OnStoppedChargingUp(float finalChargeUpPercentage)
        {
            // m_ActionViz.OnStoppedChargingUp(finalChargeUpPercentage);
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
        //     else
        //     {
        //         this.m_PartyHUD.SetAllyHealth(m_NetState.NetworkObjectId, newValue);
        //     }
        // }

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

        public void OnAnimEvent(string id)
        {
            //if you are trying to figure out who calls this method, it's "magic". The Unity Animation Event system takes method names as strings,
            //and calls a method of the same name on a component on the same GameObject as the Animator. See the "attack1" Animation Clip as one
            //example of where this is configured.

            // m_ActionViz.OnAnimEvent(id);
        }

    }
}
