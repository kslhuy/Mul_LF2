using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    
    public class PlayerAttackState : State
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;

        public PlayerAttackState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
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
            Debug.Log("AttackState");
        
        }

        public override void End()
        {
            player.stateMachine.ChangeState(StateType.Idle);
          
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
