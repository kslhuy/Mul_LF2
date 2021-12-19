// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EAttackState : AIState
// {
//     public EAttackState(AIBrain aIBrain, Player player, int IDanim) : base(aIBrain, player, IDanim)
//     {
//     }

//     public override void Enter()
//     {
//         // base.Enter();
//         startTime = Time.time;
//         player.AnimationBase.Anim.Play(player.Attack1);
//     }



//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         if (Time.time >= startTime + 0.37f){
//             aIBrain.ChangeAIState(aIBrain.EIdleState);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }

// }
