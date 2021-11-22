using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerJumpStateFX : StateFX
    {
        private int amountOfJumpLeft ;

        public PlayerJumpStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX ) : base(characterType, m_PlayerFX)
        {
        }

        public override void AnticipateState(StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Attack){
                m_PlayerFX.stateMachineViz.GetState(StateType.AttackJump1).PlayAnim(data.StateTypeEnum);
            }
        }

        public override void Enter()
        {
            base.Enter();
            if( !Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            amountOfJumpLeft--;
         }

        public override void PlayAnim(StateType currentState)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        // public override void End(){
        //     m_PlayerFX.stateMachineViz.ChangeState(StateType.Land);
        // }
        

        // public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Jump;
        }

        public override bool LogicUpdate() {
            Debug.Log("JumpStateVisual");
            return true;
        }

    }
}