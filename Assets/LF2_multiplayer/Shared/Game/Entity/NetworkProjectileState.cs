using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Serialization.Pooled;
using System.IO;
using MLAPI.Messaging;
using UnityEngine;

namespace LF2
{
    /// <summary>
    /// Shared state for a Projectile.
    /// </summary>
    public class NetworkProjectileState : NetworkBehaviour
    {
        /// <summary>
        /// This event is raised when the arrow hit an enemy. The argument is the NetworkObjectId of the enemy.
        /// </summary>
        public System.Action<ulong> HitEnemyEvent;

        [ClientRpc]
        public void RecvHitEnemyClientRPC(ulong enemyId)
        {
            HitEnemyEvent?.Invoke(enemyId);
        }
    }
}
