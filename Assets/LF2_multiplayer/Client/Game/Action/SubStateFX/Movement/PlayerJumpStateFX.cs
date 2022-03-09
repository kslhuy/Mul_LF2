using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerJumpStateFX : PlayerAirStateFX
    {
        private int amountOfJumpLeft ;

        public PlayerJumpStateFX(PlayerStateMachineFX mPlayerMachineFX ) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {            
            if (data.StateTypeEnum == StateType.Attack){
                MPlayerMachineFX.GetState(StateType.AttackJump1).PlayAnim(StateType.AttackJump1);
            }
        }

        public override void Enter()
        {
            
            if( !Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
            }
            base.Enter();
            amountOfJumpLeft--;
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
        }
        

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }


        public override void LogicUpdate() {
            MPlayerMachineFX.CoreMovement.CheckIfShouldFlip((int)MPlayerMachineFX.moveDir.x);
            base.LogicUpdate();
        }

        public override void End(){
            base.End();
        }

        public override StateType GetId()
        {
            return StateType.Jump;
        }



    }
}