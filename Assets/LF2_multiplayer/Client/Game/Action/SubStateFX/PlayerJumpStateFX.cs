using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerJumpStateFX : StateFX
    {
        private int amountOfJumpLeft ;

        public PlayerJumpStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if( !Anticipated)
            {
                PlayAnim();
            }
            amountOfJumpLeft--;
         }

        private void PlayAnim()
        {
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }
        

        // public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Jump;
        }

        public override void LogicUpdate() {
            Debug.Log("JumpStateVisual");
        }

        // public override void CanChangeState(ActionRequestData actionRequestData)
        // {
            
        // }
    }
}