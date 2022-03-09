using System;
using Unity.Netcode;

namespace LF2
{
    /// <summary>
    /// Common data and RPCs for the CharSelect stage.
    /// </summary>
    public class BackGroundSelectData : NetworkBehaviour
    {
        public NetworkVariable<int> BackGroundNumber = new NetworkVariable<int>(1);
        public NetworkVariable<bool> IsStateChooseBackGround { get; } = new NetworkVariable<bool>(false);

            // Huy Add new here 
        /// <summary>
        /// When this becomes true, the lobby is closed and in process of terminating (switching to gameplay).
        /// </summary>
        public NetworkVariable<bool> IsLobbyClosed { get; } = new NetworkVariable<bool>(false);

        public event Action< bool> OnHostChangedBackGround;

        // HOST = player 1
        public event Action OnHostStartGame;



        [ServerRpc]
        public void ChangeBackGroundServerRpc( bool nextLeft)
        {
            OnHostChangedBackGround?.Invoke( nextLeft);
        }

        // HOST = player 1
        [ServerRpc]
        public void StartGameServerRpc()
        {
            OnHostStartGame?.Invoke();
        }
    }
}