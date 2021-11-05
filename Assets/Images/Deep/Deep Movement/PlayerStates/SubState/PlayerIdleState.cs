using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{


    public class PlayerIdleState : PlayerGroundedState
    {
        bool isAttack;
        // float lastTimeAttack ;

        public PlayerIdleState(ServerCharacter player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // player.JumpState.ResetAmountOfJumpsLeft();
            isAttack = false;
        }


        public override void Exit()
        {
            base.Exit();
        }


        public override void LogicUpdate()
        {
            // base.LogicUpdate();
            // if  (player.InputHandler.canRun ){
            //     player.InputHandler.ResetRun();
            //     stateMachine.ChangeState(player.RunState);
            // }
            // else if (IsMove){
            //     stateMachine.ChangeState(player.MoveState);
            // }
            // else if (JumpInput){
            //     player.InputHandler.UseJumpInput();
            //     core.SetMovement.SetVelocityJump(playerData.jumpVelocity ,moveDir);
            //     stateMachine.ChangeState(player.JumpState);
            // }
            // else if (player.InputHandler.AttackInput && !isAttack ){
            //     // Error : Player stuck in State Attack , but animation is IDLE  
            //     stateMachine.ChangeState(player.AttackState12 , AttackType.Attack1);
            //     lastTimeAttack = Time.deltaTime;
            // }
            // else if (player.InputHandler.DefenseInput){
            //     stateMachine.ChangeState(player.DefenseState);
            // }


        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


    }
}

