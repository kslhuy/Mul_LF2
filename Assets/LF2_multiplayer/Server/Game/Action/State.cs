using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server
{
    /// <summary>
    /// The abstract parent class that all State derive from.
    /// </summary>
    /// <remarks>
    /// The State System is a generalized mechanism for Characters to "do stuff" in a networked way. State
    /// include everything from your basic character attack, to a fancy skill  Shot, 

    /// For every StateType enum, there will be one specialization of this class.

    ///
    /// The flow for State is:
    /// Initially: Enter()
    /// Every frame:   LogicUpdate() + PhysicUpdate() (can be 1 of 2)
    /// On shutdown: End() (end this State Naturelly) or Exit() (be interrupted by some logic (force to change State))
    /// After End(): Almost time will Switch to Idle .  
    ///

    // / Note also that if Start() returns false, no other functions are called on the Action, not even End().
    /// </remarks>
    public abstract class State 
    {
        protected PlayerStateMachine player;

        public StateRequestData m_Data;

        protected MovementState currentMovementState;
        

        public bool IsMove { get; private set; }

        //Vector use for All Movement of player
        protected Vector3 moveDir;

        public float TimeStarted_Server { get ; protected set;}
        protected static ulong OurNetWorkID ;

        protected State(PlayerStateMachine player)
        {
            
            this.player = player;
        }


        // Get the StateType of current State  
        public abstract StateType GetId();
        public virtual void Enter(){
            TimeStarted_Server = Time.time;
        }
        public virtual void LogicUpdate() {
        }


   

        public virtual void Exit(){ }

        // If we have a request so check if we can change to desired state 
        // NOTE :   (Only 3 basic State can check Attack , Jump , Defense)
        public virtual void  CanChangeState(StateRequestData actionRequestData){}

        public virtual void SetMovementDir(Vector2 position)
        {
            IsMove  = position.x != 0 || position.y != 0;
        }

        public virtual void End()
        {
            player.ChangeState(StateType.Idle);
        }



        public virtual void OnGameplayActivity(StateGameplayActivity activityThatOccurred)
        {
        }
    }
}