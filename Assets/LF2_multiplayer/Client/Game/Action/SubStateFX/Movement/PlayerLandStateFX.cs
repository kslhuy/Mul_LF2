using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerLandStateFX : StateFX
    {
        // private int amountOfJumpLeft = 1 ;


        public PlayerLandStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX) { }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Jump ){
                MPlayerMachineFX.m_ClientVisual.coreMovement.SetDoubleJump(MPlayerMachineFX.moveDir);
                MPlayerMachineFX.GetState(StateType.DoubleJump).PlayAnim(StateType.DoubleJump);
            }
        }

        public override void Enter( )
        {
            if( !Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Land_anim");
            }
            base.Enter();
         }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Land_anim");
        }

        public override void End(){
            MPlayerMachineFX.idle();
        }
        

        // public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

        // public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Land;
        }



        public override void LogicUpdate(){
        }




    }
}