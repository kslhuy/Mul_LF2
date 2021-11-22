using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    //In this State :  Player Stand Still  , do nothing , wait to request. 
    //                  Can do some initilize (for Exemple reset number Jump)
    public class PlayerIdleState : State
    {
        public PlayerIdleState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        // bool isAttack;
        // float lastTimeAttack ;



        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.Jump){
                player.stateMachine.ChangeState(StateType.Jump);
            }
            else if (actionRequestData.StateTypeEnum == StateType.Attack){
                player.stateMachine.ChangeState(StateType.Attack);
            } 
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (IsMove){
                player.stateMachine.ChangeState(StateType.Move);
            }

        }

     


        public override void Enter()
        {
            base.Enter();

           
            // player.JumpState.ResetAmountOfJumpsLeft();
            // isAttack = false;
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Idle;
        }



        public override void PhysicsUpdate()
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

            Debug.Log("IdleState");


        }



    }
}

