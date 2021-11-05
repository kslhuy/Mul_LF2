using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server{
    public class ActionStatePlayer
    {
         
        private ServerCharacter m_serverCharacter;

        public PlayerStateMachine StateMachine{ get; private set;}
 
        #region Player States 
        public PlayerIdleState IdleState{get; private set;}
        public PlayerMoveState MoveState{get; private set;}

        // public PlayerJumpState JumpState{get ; private set;}


        // public PlayerLandState LandState{get;private set;}
        // public PlayerAirState AirState{get; private set;}

        // public PlayerDoubleJumpState DoubleJumpState{get ; private set;}

        // public PlayerRunState RunState{get; private set;}
        // // public RunSliding SlidingState{get; private set;}

        // public PlayerAttack12 AttackState12{get;private set;}

        // public PlayerDefenseState DefenseState{get;private set;}

        // public PlayerRollingState RollingState {get; private set;}

        private ActionPlayer m_ActionPlayer;
        

        #endregion
        // Constructeur 
        public ActionStatePlayer(ServerCharacter serverCharacter){
            m_serverCharacter = serverCharacter;

            IdleState =  new PlayerIdleState(serverCharacter , StateMachine );
            MoveState =  new PlayerMoveState(serverCharacter , StateMachine);
            StateMachine = new PlayerStateMachine();
            StateMachine.Initialize(IdleState);

            m_ActionPlayer = new ActionPlayer(serverCharacter);

        } 

        public void LogicUpdate() {
            StateMachine.CurrentState.LogicUpdate();
        }





    
    
    
    }

}
