using System;
using UnityEngine;

namespace LF2.Client
{
    /// <summary>
    /// A runtime list of <see cref="PersistentPlayer"/> objects that is populated both on clients and server.
    /// </summary>
    [CreateAssetMenu]
    public class ClientPlayerAvatarRuntimeCollection : RuntimeCollection<ClientPlayerAvatar>
    {
    }
}
