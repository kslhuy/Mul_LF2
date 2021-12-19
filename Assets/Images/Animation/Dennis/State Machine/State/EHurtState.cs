// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EHurtState : AIState
// {
//     private float durationHurt = 0.5f;

//     public EHurtState(AIBrain aIBrain, Player player, int IDanim) : base(aIBrain, player, IDanim)
//     {
//     }

//     public override void Enter()
//     {
//         base.Enter();

//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         if (Time.time > startTime + durationHurt){
//             aIBrain.ChangeAIState(aIBrain.EMoveState);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }


// }
