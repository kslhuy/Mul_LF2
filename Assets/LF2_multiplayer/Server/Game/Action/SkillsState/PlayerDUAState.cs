using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerDUAState : State
    {

        bool m_ExecutionFired;
        float m_MaxDistance = 0.35f;


        private ulong m_ProvisionalTarget;


        public PlayerDUAState(PlayerStateMachine player) : base(player)
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

        public override StateType GetId()
        {
            return StateType.DUA;
        }

        public override void LogicUpdate()
        {
        }

        public override void End()
        {
            player.ChangeState(StateType.Idle);
            m_ExecutionFired = false;
        }

        public override void Exit()
        {
            m_ExecutionFired = false;
        }




    }
}
