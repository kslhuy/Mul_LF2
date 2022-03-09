using System;
using Unity.Netcode;
using UnityEngine;
using LF2;
using LF2.Server;
using LF2.Visual;

    [RequireComponent(typeof(NetworkCharacterState))]
    public class ClientDamageReceiver : NetworkBehaviour, IDamageable
    {
        [SerializeField]
        ClientCharacterVisualization m_ClientCharacterVisualization;

        /// <summary>
        /// The Visualization GameObject isn't in the same transform hierarchy as the object, but it registers itself here
        /// so that the visual GameObject can be found from a NetworkObjectId.
        /// </summary>
        public ClientCharacterVisualization ChildVizObject => m_ClientCharacterVisualization;

        public override void OnNetworkSpawn()
        {
            if (!IsClient)
            {
                enabled = false;
            }
        }

        public void SetCharacterVisualization(ClientCharacterVisualization clientCharacterVisualization)
        {
            m_ClientCharacterVisualization = clientCharacterVisualization;
        }
        public event Action<StateRequestData , int> damageReceived;

        [SerializeField]
        NetworkCharacterState m_NetworkCharacterState;

        public void ReceiveHP(StateRequestData stateRequestData, int HP)
        {
            damageReceived?.Invoke(stateRequestData , HP);

        }


        public bool IsDamageable()
        {
            return m_NetworkCharacterState.LifeState == LifeState.Alive;
        }


    }


    
