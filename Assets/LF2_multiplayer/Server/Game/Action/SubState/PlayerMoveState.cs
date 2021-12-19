using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerMoveState : State
    {
        public float SpeedWalk = 1f ;

        public PlayerMoveState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }
        
        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.Jump){
                // SetJump here because Vector moveDir is available ;
                // If we move , so wanna Jump , Jump to that direction 
                player.ServerCharacterMovement.SetJump(moveDir);
                player.stateMachine.ChangeState(StateType.Jump);
            }

        }

        public override StateType GetId()
        {
            return StateType.Move;
        }

        public override void Enter(){
            m_Data.StateTypeEnum = StateType.Move;
            // player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
            
        }
        public override void PhysicsUpdate() {

            player.ServerCharacterMovement.SetVelocityXZ(moveDir);
            player.ServerCharacterMovement.CheckIfShouldFlip((int)moveDir.x);

        }

  


        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);

            if (!IsMove){
                player.stateMachine.ChangeState(StateType.Idle);
            }
        }



    }
}
