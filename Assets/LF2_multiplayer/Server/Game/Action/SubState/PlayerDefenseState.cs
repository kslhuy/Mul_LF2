using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerDefenseState : State
    {
        private int xInput;

        public PlayerDefenseState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.DDA){
                player.stateMachine.ChangeState(actionRequestData.StateTypeEnum);
            }
        }

        public override void Enter()
        {
            base.Enter();

            m_Data.StateTypeEnum = StateType.Defense;
            // player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void End()
        {
            player.stateMachine.ChangeState(StateType.Idle);

        }

        public override StateType GetId()
        {
            return StateType.Defense;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


    }
}
