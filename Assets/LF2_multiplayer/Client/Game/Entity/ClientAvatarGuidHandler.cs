using System;
using LF2.Visual;
using Unity.Netcode;
using UnityEngine;

namespace LF2.Client
{
    /// <summary>
    /// Client-side component that awaits a state change on an avatar's Guid, and fetches matching Avatar from the
    /// AvatarRegistry, if possible. Once fetched, the Graphics GameObject is spawned.
    /// </summary>
    [RequireComponent(typeof(NetworkAvatarGuidState))]
    public class ClientAvatarGuidHandler : NetworkBehaviour
    {
        [SerializeField]
        ClientDamageReceiver m_ClientCharacter;

        [SerializeField]
        NetworkAvatarGuidState m_NetworkAvatarGuidState;

        [SerializeField]
        Animator m_GraphicsAnimator;
        public Animator graphicsAnimator => m_GraphicsAnimator;


        public event Action<GameObject> AvatarGraphicsSpawned;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Debug.Log("huy spawn");
            InstantiateAvatar();
        }

        void InstantiateAvatar()
        {
           if (m_GraphicsAnimator.transform.childCount > 0)
            {
                // we may receive a NetworkVariable's OnValueChanged callback more than once as a client
                // this makes sure we don't spawn a duplicate graphics GameObject
                return;
            }
            Instantiate(m_NetworkAvatarGuidState.RegisteredAvatar.Graphics, m_GraphicsAnimator.transform);

            m_GraphicsAnimator.Rebind();
            m_GraphicsAnimator.Update(0f);
            // var graphicsGameObject = m_NetworkAvatarGuidState.RegisteredAvatar.Graphics;

            // m_GraphicsAnimator.runtimeAnimatorController = graphicsGameObject.GetComponent<Animator>().runtimeAnimatorController;

            // Debug.Log(m_GraphicsAnimator.runtimeAnimatorController);
            // m_ClientCharacter.SetCharacterVisualization(GetComponent<ClientCharacterVisualization>());

            // AvatarGraphicsSpawned?.Invoke(m_GraphicsAnimator.gameObject);

        }
    }
}
