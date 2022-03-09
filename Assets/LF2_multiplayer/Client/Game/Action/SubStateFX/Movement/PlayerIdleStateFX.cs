using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerIdleStateFX : StateFX
    {
        public PlayerIdleStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.Jump){
                MPlayerMachineFX.m_ClientVisual.coreMovement.SetJump(Vector3.zero);
            }

            if (data.NbAnimation > 0 ){
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum, data.NbAnimation);
            }else{
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }

        }


        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (IsMove){
                MPlayerMachineFX.GetState(StateType.Move).PlayAnim(StateType.Move);
            }
        }

        public override void Enter()
        {
            if(!Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Idle_anim");  
            }
            base.Enter();
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
       
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Idle_anim");  
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Idle;
        }





    }
}

