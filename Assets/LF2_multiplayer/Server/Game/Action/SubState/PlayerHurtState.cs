// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerHurtState : PlayerState
// {
//     public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID) : base(player, stateMachine, playerData, hashID)
//     {
//     }

//     public override void Enter()
//     {
//         base.Enter();
//         // Debug.Log("Hurt");
//     }


//     public override void Exit()
//     {
//         base.Exit();
//     }



//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         if ( Time.time > startTime + 0.3f){
//             stateMachine.ChangeState(player.IdleState);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }

// }
