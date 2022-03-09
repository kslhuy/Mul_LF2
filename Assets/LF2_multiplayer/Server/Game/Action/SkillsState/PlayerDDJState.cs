using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerDDJState : State
    {

        public PlayerDDJState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {      
            base.Enter();
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }
        public override void LogicUpdate() {
        }

        public override StateType GetId()
        {
            return StateType.DDJ;
        }


        public override void End()
        {
            player.ChangeState(StateType.Idle);
        }

        public override void Exit()
        {
        }

    }
}
