using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerMoveStateFX : StateFX
    {
        public PlayerMoveStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if ( data.StateTypeEnum == StateType.Jump ){
                MPlayerMachineFX.m_ClientVisual.coreMovement.SetJump(MPlayerMachineFX.moveDir);
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
            else if (data.StateTypeEnum == StateType.Attack)
            {
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
            else if (data.StateTypeEnum == StateType.Defense){
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum);
            }
        }

 
        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);

            if (!IsMove){
                MPlayerMachineFX.GetState(StateType.Idle).PlayAnim(StateType.Idle);
            }
        }

        

        public override void Enter()
        {
            if( !Anticipated)
            {
                PlayAnim(StateType.Move);
            }
            base.Enter();
        }
        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Walk_anim");
        }

        public override StateType GetId()
        {
            return StateType.Move;
        }

        public override void LogicUpdate()
        {
            MPlayerMachineFX.CoreMovement.SetVelocityXZ(MPlayerMachineFX.moveDir);
        }
    }
}

