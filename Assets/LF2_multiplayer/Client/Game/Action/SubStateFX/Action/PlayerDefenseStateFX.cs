using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerDefenseStateFX : StateFX
    {

        public PlayerDefenseStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
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
            return StateType.Defense;
        }

        public override bool LogicUpdate()
        {
            Debug.Log("Defense Visual");
            return true;
        }


        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }


        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            Debug.Log("Defense_anim");
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Defense_anim");
        }
    }
}
