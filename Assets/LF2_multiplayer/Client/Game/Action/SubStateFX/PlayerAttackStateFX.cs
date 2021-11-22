using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackStateFX : StateFX
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;

        public PlayerAttackStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        public override void Enter()
        {


        }


        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override bool LogicUpdate()
        {

            Debug.Log("Attack Visual");
            return true;
        }


        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }
        private void FlyAttackLand()
        {
            // if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f)
            // {
            //     stateMachine.ChangeState(player.LandState);
            // }
        }

        public override void PlayAnim(StateType currentState)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Attack1_anim");
        }
    }
}
