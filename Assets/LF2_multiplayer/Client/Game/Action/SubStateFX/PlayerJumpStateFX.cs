using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerJumpStateFX : PlayerAirStateFX
    {
        private int amountOfJumpLeft ;

        public PlayerJumpStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX ) : base(characterType, m_PlayerFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            Debug.Log(data);
            
            if (data.StateTypeEnum == StateType.Attack){
                m_PlayerFX.stateMachineViz.GetState(StateType.AttackJump1).PlayAnim(StateType.AttackJump1);
            }
        }

        public override void Enter()
        {
            
            if( !Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            base.Enter();
            amountOfJumpLeft--;
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
        }
        
        public override void End(){
            base.End();
        }

 

        public override StateType GetId()
        {
            return StateType.Jump;
        }

        public override bool LogicUpdate() {
            return base.LogicUpdate();
        }

    }
}