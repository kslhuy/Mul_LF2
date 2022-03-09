using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Server
{
    // [RequireComponent(typeof(ServerCharacterMovement), typeof(NetworkCharacterState))]
    public class ServerCharacter : NetworkBehaviour
    {


        [SerializeField]
        NetworkCharacterState m_NetworkCharacterState;

        public NetworkCharacterState NetState => m_NetworkCharacterState;

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


        public PlayerStateMachine MStateMachinePlayer { get; private set; } 

        
        [SerializeField]
        ClientDamageReceiver m_DamageReceiver;

        [SerializeField]
        ServerCharacterMovement m_Movement;

        public ServerCharacterMovement Movement => m_Movement;

        [SerializeField]
        PhysicsWrapper m_PhysicsWrapper;

        public PhysicsWrapper physicsWrapper => m_PhysicsWrapper;




        private void Start()
        {
            MStateMachinePlayer = new PlayerStateMachine(this ,m_NetworkCharacterState.CharacterType);
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer) { enabled = false; }
            else
            {
                NetState.DoActionEventServer += OnActionPlayRequest;
                NetState.ReceivedClientInput += OnClientMoveRequest;
                NetState.NetworkLifeState.LifeState.OnValueChanged += OnLifeStateChanged;

                m_DamageReceiver.damageReceived += OnGameplayActivity;  

                NetState.HitPoints = NetState.CharacterClass.BaseHP.Value;

            }
        }

        public override void OnNetworkDespawn()
        {
            if (NetState)
            {
                NetState.DoActionEventServer -= OnActionPlayRequest;
                NetState.ReceivedClientInput -= OnClientMoveRequest;
                NetState.NetworkLifeState.LifeState.OnValueChanged -= OnLifeStateChanged;
            }

            if (m_DamageReceiver)
            {
                m_DamageReceiver.damageReceived -= OnGameplayActivity;
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
                MStateMachinePlayer.RequestToState(ref action);
            }
        }

        private void OnClientMoveRequest(Vector2 targetPosition)
        {
            // Player is alive or in state normal can move
            if (NetState.LifeState == LifeState.Alive && !m_Movement.IsPerformingForcedMovement())
            {
                // not move in sever 
                // only use to transition state  
                MStateMachinePlayer.SetMovementDirection(targetPosition);
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
        public void OnGameplayActivity(StateRequestData stateRequestData, int HP)
        {
            MStateMachinePlayer.OnGameplayActivity(stateRequestData);
                        // 
            m_NetworkCharacterState.HitPoints = Mathf.Min(m_NetworkCharacterState.CharacterClass.BaseHP.Value, m_NetworkCharacterState.HitPoints+HP);

            //we can't currently heal a dead character back to Alive state.
            //that's handled by a separate function.
            if (m_NetworkCharacterState.HitPoints <= 0)
            {
                m_NetworkCharacterState.LifeState = LifeState.Fainted;
            }
        }


        void Update()
        {

            MStateMachinePlayer.Update();

        }



    }
}
