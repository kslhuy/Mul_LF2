using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace LF2.Server{

    public class PlayerHurtState : State
    {

        private int m_nbHurt; 

        public PlayerHurtState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            m_nbHurt += 1;

            m_Data.StateTypeEnum = StateType.Hurt;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

        }


        public override void End()
        {
            player.ChangeState(StateType.Idle);

        }

        public override StateType GetId()
        {
            return StateType.Hurt;
        }

    }
 }
