using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerLandStateFX : StateFX
    {
        private int amountOfJumpLeft ;

        public PlayerLandStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Jump){
                m_PlayerFX.stateMachineViz.GetState(StateType.DoubleJump).PlayAnim(StateType.DoubleJump);
            }
        }

        public override void Enter( )
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
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Land_anim");
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }
        

        // public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Land;
        }



        public override bool LogicUpdate(){
            // Debug.Log("Land Visual");
            return true;
        }




    }
}