using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackJump1FX : PlayerAirStateFX
    {
        float attack12distance;

        public PlayerAttackJump1FX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.AttackJump1;
        }

        public override bool LogicUpdate()
        {
            base.LogicUpdate();
            return true;
        }


        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }

        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("AttackJump1_anim");
        }
    }
}
