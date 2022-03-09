using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackJump1FX : PlayerAirStateFX
    {
        float attack12distance;

        public PlayerAttackJump1FX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(StateType.AttackJump1);
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.AttackJump1;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }


        public override void End(){
            MPlayerMachineFX.idle();
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("AttackJump1_anim");
        }
    }
}
