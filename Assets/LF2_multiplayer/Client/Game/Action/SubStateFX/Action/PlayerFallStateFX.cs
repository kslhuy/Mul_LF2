using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace LF2.Visual{

/// NEED TO Grounded to take regain control 
    public class PlayerFallStateFX : StateFX
    {
        public PlayerFallStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void Enter()
        {
            if( !Anticipated)
            {
                PlayAnim(GetId() , 0) ;
            }            
            base.Enter();
        }



        public override void LogicUpdate()
        {      
            if (MPlayerMachineFX.CoreMovement.IsGounded()) 
            {
                if (Time.time - TimeStarted_Animation > 0.5f){
                    MPlayerMachineFX.ChangeState(StateType.Idle);
                }
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PlayAnim(StateType currentState , int nbanim )
        {
            base.PlayAnim(currentState,nbanim);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Fall_1_1anim");

        }

        public override StateType GetId()
        {
            return StateType.Fall;
        }


    }
 }
