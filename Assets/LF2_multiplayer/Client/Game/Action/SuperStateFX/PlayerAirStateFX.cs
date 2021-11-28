// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// namespace LF2.Visual{

//     public class PlayerAirStateFX : StateFX
//     {
//         private int amountOfJumpLeft ;

//         public PlayerAirStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX ) : base(characterType, m_PlayerFX)
//         {
//         }

//         public override void AnticipateState(StateRequestData data)
//         {

//         }

//         public override void Enter()
//         {
//             base.Enter();
//         }

//         public bool CanJump(){
//             if (amountOfJumpLeft > 0){
//                 return true;
//             }else return false;
//         }


//         public void DecreaseAmountOfJumpsLeft()=>amountOfJumpLeft--;

//         public override StateType GetId()
//         {
//             return StateType.Air;
//         }

//         public override bool LogicUpdate() {
//             return true;
//         }

//     }
// }