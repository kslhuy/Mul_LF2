using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

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
            // Debug.Log("SetJump");
            player.ServerCharacterMovement.SetJump(workSpace);

            // amountOfJumpLeft--;
         }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        public override void PhysicsUpdate() {

            Debug.Log("JumpState");
            player.ServerCharacterMovement.SetFallingDown();
            // player.ServerCharacterMovement.IsGounded();
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

        public override void CanChangeState(ActionRequestData actionRequestData)
        {
            if (actionRequestData.ActionTypeEnum == ActionType.JumpGeneral ){
                player.stateMachine.ChangeState(StateType.Jump);
            }
        }

    }
}