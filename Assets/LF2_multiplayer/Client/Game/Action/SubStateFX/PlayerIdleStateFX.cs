using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerIdleStateFX : StateFX
    {
        public PlayerIdleStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        // public override void CanChangeState(ActionRequestData actionRequestData)
        // {

        // }

        public override void AnticipateState(ActionRequestData data)
        {
            if (data.ActionTypeEnum == ActionType.AttackGeneral){
                // m_PlayerFX.stateMachineViz.GetState(StateType.Attack).
                m_PlayerFX.m_ClientVisual.OurAnimator.Play("Attack1_anim");
            }

        }


        public override void SetMovementTarget(Vector2 position)
        {
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Move);
        }



        public override void Enter()
        {
            base.Enter();
            // player.JumpState.ResetAmountOfJumpsLeft();
        }

        private void PlayAnim()
        {
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Idle_anim");
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Idle;
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

            Debug.Log("IdleStateVisual");


        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


    }
}

