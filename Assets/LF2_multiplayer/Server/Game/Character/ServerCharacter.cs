using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Server
{
    [RequireComponent(typeof(ServerCharacterMovement), typeof(NetworkCharacterState))]
    public class ServerCharacter : NetworkBehaviour, IDamageable
    {
        public NetworkCharacterState NetState { get; private set; }

        /// <summary>
        /// Returns true if this Character is an NPC.
        /// </summary>
        public bool IsNpc
        {
            get { return NetState.IsNpc; }
        }


        [SerializeField]
        [Tooltip("If set to false, an NPC character will be denied its brain (won't attack or chase players)")]
        private bool m_BrainEnabled = true;

        [SerializeField]
        [Tooltip("Setting negative value disables destroying object after it is killed.")]
        private float m_KilledDestroyDelaySeconds = 3.0f;


        private PlayerState m_statePlayer;


        // ***** ***


        // private AIBrainNew m_AIBrain;

        // Cached component reference
        private ServerCharacterMovement m_Movement;
        


        private void Awake()
        {
            m_Movement = GetComponent<ServerCharacterMovement>();
            NetState = GetComponent<NetworkCharacterState>();

            m_statePlayer = new PlayerState(this,m_Movement);

            
            // if (IsNpc)
            // {
            //     m_AIBrain = new AIBrainNew(this);
            // }
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer) { enabled = false; }
            else
            {
                NetState = GetComponent<NetworkCharacterState>();
                NetState.DoActionEventServer += OnActionPlayRequest;
                NetState.ReceivedClientInput += OnClientMoveRequest;
                // NetState.OnStopChargingUpServer += OnStoppedChargingUp;
                NetState.NetworkLifeState.LifeState.OnValueChanged += OnLifeStateChanged;

                // if (NetState.IsNpc)
                // {
                //     m_AIBrain = new AIBrain(this, m_ActionPlayer);
                // }

                NetState.HitPoints = NetState.CharacterClass.BaseHP.Value;

            }
        }

        public void OnDestroy()
        {
            if (NetState)
            {
                NetState.DoActionEventServer -= OnActionPlayRequest;
                NetState.ReceivedClientInput -= OnClientMoveRequest;
                // NetState.OnStopChargingUpServer -= OnStoppedChargingUp;
                NetState.NetworkLifeState.LifeState.OnValueChanged -= OnLifeStateChanged;
            }
        }

        // /// <summary>
        // /// Play a sequence of actions!
        // /// </summary>
        public void PlayAction(ref StateRequestData action)
        {
            //the character needs to be alive and not be state uncontrol =>  in order to be able to play actions
            if (NetState.LifeState == LifeState.Alive && !m_Movement.IsPerformingForcedMovement())
            {
                m_statePlayer.RequestToState(ref action);
            }
        }

        private void OnClientMoveRequest(Vector2 targetPosition)
        {
            if (NetState.LifeState == LifeState.Alive && !m_Movement.IsPerformingForcedMovement())
            {
                m_statePlayer.SetMovementDirection(targetPosition);
            }
        }

        private void OnLifeStateChanged(LifeState prevLifeState, LifeState lifeState)
        {
            if (lifeState != LifeState.Alive)
            {
                m_Movement.CancelMove();
            }
        }

        private void OnActionPlayRequest(StateRequestData data)
        {
            // Debug.Log("StateRequestData");
            PlayAction(ref data);

        }

        IEnumerator KilledDestroyProcess()
        {
            yield return new WaitForSeconds(m_KilledDestroyDelaySeconds);

            if (NetworkObject != null)
            {
                NetworkObject.Despawn(true);
            }
        }

        /// <summary>
        /// Receive an HP change from somewhere. Could be healing or damage.
        /// </summary>
        /// <param name="inflicter">Person dishing out this damage/healing. Can be null. </param>
        /// <param name="HP">The HP to receive. Positive value is healing. Negative is damage.  </param>
        public void ReceiveHP(ServerCharacter inflicter, int HP)
        {
            // Debug.Log("Receive HP");
       
             //to our own effects, and modify the damage or healing as appropriate. But in this game, we just take it straight.
            if (HP > 0)
            {
                m_statePlayer.OnGameplayActivity(State.GameplayActivity.Healed);
                // float healingMod = m_ActionPlayer.GetBuffedValue(Action.BuffableValue.PercentHealingReceived);
                // HP = (int)(HP * healingMod);
            }
            else
            {
                m_statePlayer.OnGameplayActivity(State.GameplayActivity.AttackedByEnemy);
                // float damageMod = m_statePlayer.GetBuffedValue(Action.BuffableValue.PercentDamageReceived);

                // serverAnimationHandler.NetworkAnimator.SetTrigger("HitReact1");
            }

            NetState.HitPoints = Mathf.Min(NetState.CharacterClass.BaseHP.Value, NetState.HitPoints+HP);

            // if( m_AIBrain != null )
            // {
            //     //let the brain know about the modified amount of damage we received.
            //     m_AIBrain.ReceiveHP(inflicter, HP);
            // }

            // //we can't currently heal a dead character back to Alive state.
            // //that's handled by a separate function.
            // if (NetState.HitPoints <= 0)
            // {
            //     m_ActionPlayer.ClearActions(false);

            //     if (IsNpc)
            //     {
            //         if (m_KilledDestroyDelaySeconds >= 0.0f && NetState.LifeState != LifeState.Dead)
            //         {
            //             StartCoroutine(KilledDestroyProcess());
            //         }

            //         NetState.LifeState = LifeState.Dead;
            //     }
            //     else
            //     {
            //         NetState.LifeState = LifeState.Fainted;
            //     }
            // }
        }

        /// <summary>
        /// Determines a gameplay variable for this character. The value is determined
        /// by the character's active Actions.
        /// </summary>
        /// <param name="buffType"></param>
        /// <returns></returns>
        // public float GetBuffedValue(Action.BuffableValue buffType)
        // {
        //     return m_ActionPlayer.GetBuffedValue(buffType);
        // }

        /// <summary>
        /// Receive a Life State change that brings Fainted characters back to Alive state.
        /// </summary>
        /// <param name="inflicter">Person reviving the character.</param>
        /// <param name="HP">The HP to set to a newly revived character.</param>
        public void Revive(ServerCharacter inflicter, int HP)
        {
            Debug.Log("Revive");
            // if (NetState.LifeState == LifeState.Fainted)
            // {
            //     NetState.HitPoints = Mathf.Clamp(HP, 0, NetState.CharacterData.BaseHP.Value);
            //     NetState.NetworkLifeState.LifeState.Value = LifeState.Alive;
            // }
        }

        void Update()
        {

            m_statePlayer.Update();
            // ********** AI ***********
            // if (m_AIBrain != null && NetState.LifeState == LifeState.Alive && m_BrainEnabled)
            // {
            //     m_AIBrain.currentState.LogicUpdate();
            // }
        }

        private void FixedUpdate() {
            m_statePlayer.PhysicsUpdate();
        }



        private void OnCollisionEnter(Collision collision)
        {
            // if (m_ActionPlayer != null)
            // {
            //     m_ActionPlayer.OnCollisionEnter(collision);
            // }
        }

        // private void OnStoppedChargingUp()
        // {
        //     m_ActionPlayer.OnGameplayActivity(Action.GameplayActivity.StoppedChargingUp);
        // }

        // public IDamageable.SpecialDamageFlags GetSpecialDamageFlags()
        // {
        //     return IDamageable.SpecialDamageFlags.None;
        // }

        public bool IsDamageable()
        {
            return NetState.NetworkLifeState.LifeState.Value == LifeState.Alive;
        }

        /// <summary>
        /// This character's AIBrain. Will be null if this is not an NPC.
        /// </summary>
        // public AIBrainNew AIBrain { get { return m_AIBrain; } }
    }
}
