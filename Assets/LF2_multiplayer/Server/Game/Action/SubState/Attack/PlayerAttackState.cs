using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    
    public class PlayerAttackState : State
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;

        public PlayerAttackState(PlayerState player, SetMovement setMovement) : base(player, setMovement)
        {
        }

        public override void CanChangeState(ActionRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {
            player.serverplayer.NetState.RecvDoActionClientRPC(m_ActionRequestData);
        }

        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override void PhysicsUpdate()
        {
            // switch (stateMachine.CurrentState.attackType)
            // {
            //     case AttackType.Attack1:
            //         AttackToIdle();
            //         break;
            //     case AttackType.Attack3:
            //         AttackToIdle();
            //         break;
            //     case AttackType.Attack4:
            //         FlyAttackLand();
            //         break;
            //     case AttackType.Attack5:
            //         FlyAttackLand();
            //         break;
            // }
            AttackToIdle();
        
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
