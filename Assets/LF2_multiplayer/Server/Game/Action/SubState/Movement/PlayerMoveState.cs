using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerMoveState : State
    {


        public PlayerMoveState(PlayerStateMachine player) : base(player)
        {
        }
        
        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.Jump){
                // SetJump here because Vector moveDir is available ;
                // If we move , so wanna Jump , Jump to that direction 
                player.ChangeState(StateType.Jump);
            }
            else if(actionRequestData.StateTypeEnum == StateType.Attack || actionRequestData.StateTypeEnum == StateType.Defense )              
                player.ChangeState(actionRequestData.StateTypeEnum);
            

        }

        public override StateType GetId()
        {
            return StateType.Move;
        }

        public override void Enter(){
            m_Data.StateTypeEnum = StateType.Move;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);
        }

        public override void LogicUpdate(){
            player.ServerCharacterMovement.SetVelocityXZ(player.moveDir);
        }


        
        public override void SetMovementDir(Vector2 position)
        {
            base.SetMovementDir(position);

            if (!IsMove){
                player.ChangeState(StateType.Idle);
            }
        }



    }
}
