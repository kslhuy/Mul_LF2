using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{
    //In this State :  Player Jump in to air  , can change to desied State follow some request. 
    //                 Do a jump physics , and check every frame  player touch Ground
    public class PlayerJumpState : PlayerAirState
    {
        private int amountOfJumpLeft ;
        float timeStartJump;

        public PlayerJumpState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.Attack ){
                player.stateMachine.ChangeState(StateType.AttackJump1);
            }
        }

        public override void Enter()
        {
            base.Enter();
            m_Data.StateTypeEnum = StateType.Jump;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
            Debug.Log("JumpState");
 
        }
        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }



        public override StateType GetId()
        {
            return StateType.Jump;
        }

        

    }
}