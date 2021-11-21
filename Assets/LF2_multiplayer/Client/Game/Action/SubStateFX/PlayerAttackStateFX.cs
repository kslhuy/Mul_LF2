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


        // public override void CanChangeState(ActionRequestData actionRequestData)
        // {
        // }

        public override void Enter()
        {


        }
        private void PlayAnim()
        {
            // m_PlayerFX.m_ClientVisual.OurAnimator.Play("Jump_anim");
        }

        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override void LogicUpdate()
        {

            Debug.Log("Attack Visual");
        
        }


        private void AttackToIdle()
        {
            // TODO 
            // if (isAnimationFinished || isFinishedAnimation())
            // {
            //     isAnimationFinished = false;
            //     stateMachine.ChangeState(player.IdleState);
            // }

            // bool expirable = action.Description.DurationSeconds > 0f; //non-positive value is a sentinel indicating the duration is indefinite.
            // bool timeExpired = expirable && action.TimeRunning >= action.Description.DurationSeconds;
            // bool timedOut = !action.Anticipated && action.TimeRunning >= k_AnticipationTimeoutSeconds;

        }

        private void FlyAttackLand()
        {
            // if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f)
            // {
            //     stateMachine.ChangeState(player.LandState);
            // }
        }



    }
}
