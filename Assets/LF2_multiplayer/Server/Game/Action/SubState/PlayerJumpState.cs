using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{
    //In this State :  Player Jump in to air  , can change to desied State follow some request. 
    //                 Do a jump physics , and check every frame  player touch Ground
    public class PlayerJumpState : State
    {
        private int amountOfJumpLeft ;
        float timeStartJump;

        public PlayerJumpState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
  
            timeStartJump = Time.time ;
            player.ServerCharacterMovement.SetJump(workSpace);
            m_ActionRequestData.StateTypeEnum = StateType.Jump;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_ActionRequestData);
            // amountOfJumpLeft--;
         }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        public override void PhysicsUpdate() {

            Debug.Log("JumpState");
            // Add some gravity for player
            player.ServerCharacterMovement.SetFallingDown();
            // Check play touched ground ?? 
            if (player.ServerCharacterMovement.IsGounded() && Time.time - timeStartJump > 0.5f ){
                player.stateMachine.ChangeState(StateType.Land);
            }
        }



        // public void ResetAmountOfJumpsLeft()=> amountOfJumpLeft = playerData.amountOfJumpLeft;

        public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

        public override StateType GetId()
        {
            return StateType.Jump;
        }

        
        public override void CanChangeState(StateRequestData actionRequestData)
        {
            if (actionRequestData.StateTypeEnum == StateType.Jump ){
                player.stateMachine.ChangeState(StateType.Jump);
            }
        }

    }
}