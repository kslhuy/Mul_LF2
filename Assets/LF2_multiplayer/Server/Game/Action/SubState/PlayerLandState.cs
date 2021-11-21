using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{
    public class PlayerLandState : State
    {
        float timenow;
        public PlayerLandState(PlayerState player, SetMovement setMovement) : base(player, setMovement)
        {
        }

        public override void CanChangeState(ActionRequestData actionRequestData)
        {

        }

        public override void Enter()
        {
            base.Enter();
            timenow = Time.time;
        }

        public override StateType GetId()
        {
            return StateType.Land;
        }

        public override void PhysicsUpdate()
        {

            // if (JumpInput && player.JumpState.CanJump()){
                
            //     stateMachine.ChangeState(player.DoubleJumpState);
            // }
            // else if (DefenseInput)
            // {
            //     stateMachine.ChangeState(player.RollingState);
            // }
            // else if(isAnimationFinished ) {
            //     stateMachine.ChangeState(player.IdleState);
            // }
            Debug.Log("Land");
            if (Time.time - timenow >0.2f){
                player.stateMachine.ChangeState(StateType.Idle );

            }
        }


    }
}
