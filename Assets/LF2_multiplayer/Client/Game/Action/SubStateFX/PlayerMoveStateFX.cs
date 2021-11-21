using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerMoveStateFX : StateFX
    {
        public PlayerMoveStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }

        // public override void CanChangeState(ActionRequestData actionRequestData)
        // {
        //     if (actionRequestData.ActionTypeEnum == ActionType.AttackGeneral){
        //         m_PlayerFX.stateMachineViz.ChangeState(StateType.Attack);
        //     }
        //     if (actionRequestData.ActionTypeEnum == ActionType.JumpGeneral){
        //         m_PlayerFX.stateMachineViz.ChangeState(StateType.Jump);
        //     }

        // }

        public override void SetMovementTarget(Vector2 position)
        {
            if (position == Vector2.zero ){
                m_PlayerFX.m_ClientVisual.OurAnimator.Play("Idle_anim");
                m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
            }
        }



        public override void Enter()
        {
            base.Enter();
            if( !Anticipated)
            {
                PlayAnim();
            }
        }
        private void PlayAnim()
        {
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Move;
        }



        public override void LogicUpdate()
        {
            Debug.Log("MoveState Visual");
        }



        // public override void PhysicsUpdate()
        // {
        //     base.PhysicsUpdate();
        // }


    }
}

