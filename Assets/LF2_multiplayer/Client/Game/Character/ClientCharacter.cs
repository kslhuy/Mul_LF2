using UnityEngine;

namespace LF2.Client
{
    [RequireComponent(typeof(LF2.NetworkCharacterState))]
    public class ClientCharacter : MLAPI.NetworkBehaviour
    {
        /// <summary>
        /// The Vizualization GameObject isn't in the same transform hierarchy as the object, but it registers itself here
        /// so that the visual GameObject can be found from a NetworkObjectId.
        /// </summary>
        public LF2.Visual.ClientCharacterVisualization ChildVizObject { get; set; }

        public override void NetworkStart()
        {
            if (!IsClient) { this.enabled = false; }
        }

    }

}
