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

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Attack || data.StateTypeEnum == StateType.Jump ){
                Anticipated = true;
                m_PlayerFX.stateMachineViz.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
        }

 
        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (!IsMove){
                m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
            }
        }

        

        public override void Enter()
        {
            if( !Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            base.Enter();
        }
        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            // m_PlayerFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
        }

        public override StateType GetId()
        {
            return StateType.Move;
        }



        public override bool LogicUpdate()
        {
            m_PlayerFX.m_ClientVisual.coreMovement.SetVelocityXZ(moveDir);
            return true;
        }
    }
}

