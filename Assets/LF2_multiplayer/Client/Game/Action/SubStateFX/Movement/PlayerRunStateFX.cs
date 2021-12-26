using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerRunStateFX : StateFX
    {
        private float runVelocity ;

        public PlayerRunStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        public override void AnticipateState(ref StateRequestData requestData)
        {

            if (requestData.StateTypeEnum == StateType.Jump){
                m_PlayerFX.stateMachineViz.ChangeState(StateType.DoubleJump);
            }
            else if (requestData.StateTypeEnum == StateType.Defense){
                m_PlayerFX.stateMachineViz.ChangeState(StateType.Rolling);
            }
            else if (requestData.StateTypeEnum == StateType.Attack)
            {
                m_PlayerFX.stateMachineViz.ChangeState(StateType.Attack);
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



        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            Debug.Log(moveDir.z);
        }



        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);
        }


        public override void End()
        {
            base.End();
        }

        public override bool LogicUpdate()
        {
            m_PlayerFX.m_ClientVisual.coreMovement.SetRun(2f);
            if ( moveDir.z >0.9f || moveDir.z < -0.9f  ){
                m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
            }

            return true;
        }
    }
}
