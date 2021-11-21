// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerDefenseState : PlayerState
// {
//     private int xInput;

//     public PlayerDefenseState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
//     {
//     }



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
//         xInput = Mathf.RoundToInt(player.InputHandler.RawMovementInput.x);
//         core.SetMovement.CheckIfShouldFlip(xInput);
//         if (isFinishedAnimation()){
//             stateMachine.ChangeState(player.IdleState);
//         }

//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }


// }
