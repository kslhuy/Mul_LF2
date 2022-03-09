using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerRunState : State
    {
        private float runVelocity ;

        public PlayerRunState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {

            if (actionRequestData.StateTypeEnum == StateType.Jump ||
                actionRequestData.StateTypeEnum == StateType.Attack){
                player.ChangeState(actionRequestData.StateTypeEnum );
            }
            else if (actionRequestData.StateTypeEnum == StateType.Defense)
                player.ChangeState(StateType.Rolling);
           
        }

        public override void Enter()
        {
            m_Data.StateTypeEnum = StateType.Run;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
            // ko cho nhay lan thu 2 khi Run
            // player.JumpState.DecreaseAmountOfJumpsLeft();
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Run;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if ( player.moveDir.z >0.9f || player.moveDir.z < -0.9f  ){
                player.ChangeState(StateType.Sliding);
            }
        }



    }
}
