using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{

    public class PlayerMoveState : State
    {
        public float SpeedWalk = 1f ;

        public PlayerMoveState(PlayerState player, SetMovement setMovement) : base(player, setMovement)
        {
        }

        public override StateType GetId()
        {
            return StateType.Move;
        }

        public override void Enter(){

        }
        public override void PhysicsUpdate() {
            // if (currentMovementState == MovementState.Move){
            //     // setMovement.SetVelocityXZ(workSpace.x , workSpace.y);
            //     Debug.Log("Run");
            //     // serverCharacterMovement.transform.position += workSpace*Time.deltaTime*SpeedWalk;

            // }
            // else {
            //     player.stateMachine.ChangeState(StateType.Idle);
            // }
            Debug.Log("MoveState");
            // setMovement.SetVelocityXZ(workSpace);
            player.ServerCharacterMovement.SetVelocityXZ(workSpace);
            // Debug.Log(IsMove);
            
        }

  

        public override void CanChangeState(ActionRequestData actionRequestData)
        {
            if (actionRequestData.ActionTypeEnum == ActionType.JumpGeneral){
                player.stateMachine.ChangeState(StateType.Jump);
            }

        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
            if (!IsMove){
                player.stateMachine.ChangeState(StateType.Idle);
            }
        }


        // public void CheckIfShouldFlip(int xInput){
        //     if (xInput != 0 && xInput != FacingDirection){
        //         Flip();
        //     }
        // }
        // public void Flip(){
        //     FacingDirection *=-1;
        //     transform.Rotate(0.0f,180.0f,0.0f);
        // }
    



        //     public override void Enter()
        //     {
        //         base.Enter();
        //     }

        //     public override void Exit()
        //     {
        //         base.Exit();
        //     }

        //     public override void LogicUpdate()
        //     {
        //         base.LogicUpdate();
        //         core.SetMovement.CheckIfShouldFlip(xInput);
        //         if (!IsMove){
        //             stateMachine.ChangeState(player.IdleState);
        //         }
        //         else if (JumpInput){
        //             player.InputHandler.UseJumpInput();
        //             core.SetMovement.SetVelocityJump(playerData.jumpVelocity ,moveDir);
        //             stateMachine.ChangeState(player.JumpState);
        //         }

        //     }

        //     public override void PhysicsUpdate()
        //     {
        //         base.PhysicsUpdate();
        //         core.SetMovement.SetVelocityXZ(playerData.movementVelocityX * xInput, playerData.movementVelocityZ * zInput);
        //     }

    }
}
