using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    //in this State :  Player Actack  , can change to desied State follow some request. 
    //                 It is not explicitly targeted (so can attack to all time ), but rather detects the foe (enemy ) that was hit with a physics check.
    public class PlayerAttackState : State
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;

        public PlayerAttackState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {          
            TimeStarted = Time.time;

            m_ActionRequestData.StateTypeEnum = StateType.Attack;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_ActionRequestData);
          
        }

        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override void PhysicsUpdate()
        {
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
