using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerAirStateFX : StateFX
    {
        private int amountOfJumpLeft ;

        public PlayerAirStateFX(PlayerStateMachineFX mPlayerMachineFX ) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        
        public override void LogicUpdate() {

            
            // Debug.Log($" Air_FX = {Time.time - TimeStarted_Animation}"); 
            if (MPlayerMachineFX.CoreMovement.IsGounded() && Time.time - TimeStarted_Animation > 0.2f ){
                MPlayerMachineFX.GetState(StateType.Land).PlayAnim(StateType.Land);
            }
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }




        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Air;
        }


        

    }
}