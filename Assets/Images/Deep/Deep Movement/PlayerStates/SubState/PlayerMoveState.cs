using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerMoveState : PlayerGroundedState
    {

        public PlayerMoveState(ServerCharacter player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        //     public override void Enter()
        //     {
        //         base.Enter();
        //     }

        //     public override void Exit()
        //     {
        //         base.Exit();
        //     }

        //     public override void LogicUpdate()
        //     {
        //         base.LogicUpdate();
        //         core.SetMovement.CheckIfShouldFlip(xInput);
        //         if (!IsMove){
        //             stateMachine.ChangeState(player.IdleState);
        //         }
        //         else if (JumpInput){
        //             player.InputHandler.UseJumpInput();
        //             core.SetMovement.SetVelocityJump(playerData.jumpVelocity ,moveDir);
        //             stateMachine.ChangeState(player.JumpState);
        //         }

        //     }

        //     public override void PhysicsUpdate()
        //     {
        //         base.PhysicsUpdate();
        //         core.SetMovement.SetVelocityXZ(playerData.movementVelocityX * xInput, playerData.movementVelocityZ * zInput);
        //     }
  
    }
}
