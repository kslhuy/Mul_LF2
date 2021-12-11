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
    public abstract class State : StateBase
    {
        protected PlayerState player;

        protected StateRequestData m_Data;

        protected MovementState currentMovementState;
        

        // protected StateRequestData m_ActionRequestData;

        public bool IsMove { get; private set; }

        //Vector use for All Movement of player
        protected Vector3 moveDir;


        protected static ulong OurNetWorkID ;

        protected State(CharacterTypeEnum characterType, PlayerState player) : base(characterType)
        {
            this.player = player;
        }

        

        // Get the StateType of current State  
        public abstract StateType GetId();
        public virtual void Enter(){
            TimeStarted = Time.time;
        }
        public virtual void LogicUpdate() {}

        public virtual void PhysicsUpdate(){}
   

        public virtual void Exit(){
        }

        // If we have a request so check if we can change to desired state 
        // NOTE :   (Only 3 basic State can check Attack , Jump , Defense)
        public abstract void  CanChangeState(StateRequestData actionRequestData);

        public virtual void SetMovementTarget(Vector2 position)
        {
            moveDir.Set(position.x , 0, position.y);   
            IsMove  = position.x != 0 || position.y != 0;
        }

        public virtual void End()
        {
        }

        public enum GameplayActivity
        {
            AttackedByEnemy,
            Healed,
            StoppedChargingUp,
            UsingAttackAction, // called immediately before we perform the attack Action
        }

        public virtual void OnGameplayActivity(GameplayActivity activityThatOccurred)
        {
        }
    }
}