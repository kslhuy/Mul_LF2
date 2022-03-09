using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace LF2.Server{

    public class PlayerFallState : State
    {

        private int m_nbHurt; 

        public PlayerFallState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            m_Data.StateTypeEnum = StateType.Fall;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }

        public override void LogicUpdate()
        {
            if (player.ServerCharacterMovement.IsGounded()){

                if (Time.time - TimeStarted_Server > 0.5f ){
                    player.ChangeState(StateType.Idle);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


        public override StateType GetId()
        {
            return StateType.Fall;
        }

    }
 }
