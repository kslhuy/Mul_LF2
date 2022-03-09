using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    public class PlayerRollingState : State
    {

        public PlayerRollingState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            m_Data.StateTypeEnum = StateType.Rolling;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override StateType GetId()
        {
            return StateType.Rolling;
        }

        public override void LogicUpdate()
        {
            
        }
    }
}
