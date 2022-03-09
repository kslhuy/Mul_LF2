using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    //In this State :  Player Jump in to air  , can change to desied State follow some request. 
    //                 Do a jump physics , and check every frame  player touch Ground
    public class PlayerAirState : State
    {
        private int amountOfJumpLeft ;

        public float AirStateTimeStarted { get; private set; }

        public PlayerAirState(PlayerStateMachine player) : base(player)
        {
        }

        public override void Enter()
        {
            AirStateTimeStarted = Time.time;
        }

        public bool CanJump(){
            if (amountOfJumpLeft > 0){
                return true;
            }else return false;
        }

        public override void LogicUpdate() {

            // Add some gravity for player
            // // Check play touched ground ?? 
            // Debug.Log($" Air_Server = {Time.time - AirStateTimeStarted}"); 
            if (player.ServerCharacterMovement.IsGounded() && Time.time - AirStateTimeStarted > 0.3f   ){
                player.ChangeState(StateType.Land);
            }
        }

        public override StateType GetId()
        {
            return StateType.Air;
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
        }
    }
}