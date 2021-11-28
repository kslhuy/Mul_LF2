using UnityEngine;
using LF2.Visual;

namespace LF2.Client
{
    [RequireComponent(typeof(LF2.NetworkCharacterState))]
    public class ClientCharacter : MLAPI.NetworkBehaviour
    {
        [SerializeField]
        ClientCharacterVisualization m_ClientCharacterVisualization;

        /// <summary>
        /// The Visualization GameObject isn't in the same transform hierarchy as the object, but it registers itself here
        /// so that the visual GameObject can be found from a NetworkObjectId.
        /// </summary>
        public ClientCharacterVisualization ChildVizObject => m_ClientCharacterVisualization;

        public override void NetworkStart()
        {
            if (!IsClient) { this.enabled = false; }
        }

    }

}
