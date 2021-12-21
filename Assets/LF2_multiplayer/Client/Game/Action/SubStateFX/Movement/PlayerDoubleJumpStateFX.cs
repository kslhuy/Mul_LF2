using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerDoubleJumpStateFX : PlayerAirStateFX
    {

        public PlayerDoubleJumpStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX ) : base(characterType, m_PlayerFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
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
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("DoubleJump_anim");
        }

        
        public override void End(){
            base.End();
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }


        public override StateType GetId()
        {
            return StateType.DoubleJump;
        }

        public override bool LogicUpdate() {
            return base.LogicUpdate();
        }

    }
}