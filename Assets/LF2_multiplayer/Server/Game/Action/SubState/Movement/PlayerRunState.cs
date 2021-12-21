using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerRunState : State
    {
        private float runVelocity ;

        public PlayerRunState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if ( moveDir.z >0.9f || moveDir.z < -0.9f  ){
                player.stateMachine.ChangeState(StateType.Sliding);
            }
            else if (actionRequestData.StateTypeEnum == StateType.Jump){
                player.stateMachine.ChangeState(StateType.DoubleJump);
            }
            else if (actionRequestData.StateTypeEnum == StateType.Defense){
                player.stateMachine.ChangeState(StateType.Rolling);
            }
            else if (actionRequestData.StateTypeEnum == StateType.Attack)
            {
                player.stateMachine.ChangeState(StateType.Attack);
            }        
        }

        public override void Enter()
        {
            base.Enter();
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
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            // core.SetMovement.SetVelocityRun(runVelocity);

        }


    }
}
