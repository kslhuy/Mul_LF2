// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// namespace LF2.Server{
//     public class PlayerGroundedState : PlayerState{

    
//     protected Vector3 moveDir;

//     protected bool IsMove;
//     protected bool JumpInput;

//     public PlayerGroundedState(ServerCharacter player, PlayerStateMachine stateMachine) : base(player, stateMachine)
//     {

//     }

//     protected bool DefenseInput {get ; private set;}

//     protected bool AttackInput { get; private set; }




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
//         // xInput = Mathf.RoundToInt(player.InputHandler.RawMovementInput.x); 
//         // zInput = Mathf.RoundToInt(player.InputHandler.RawMovementInput.y);
//         // moveDir = new Vector3(xInput , 0 , zInput);
//         // IsMove = xInput != 0 || zInput != 0;
//         // JumpInput = player.InputHandler.JumpInput;
//         // DefenseInput = player.InputHandler.DefenseInput;
//         // AttackInput = player.InputHandler.AttackInput;

//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }


// }

// }
