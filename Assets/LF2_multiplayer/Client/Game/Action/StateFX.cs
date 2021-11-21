using UnityEngine;
using System.Collections.Generic;
using System;

namespace LF2.Visual{

    public abstract class StateFX: StateBase{

        protected PlayerStateFX m_PlayerFX;


        protected StateFX(PlayerStateFX m_PlayerFX) 
        {
            this.m_PlayerFX = m_PlayerFX;

        }

        public bool Anticipated { get; protected set; }


        public abstract StateType GetId();
        public virtual void Enter(){
            Anticipated = false; //once you start for real you are no longer an anticipated action.
            TimeStarted = UnityEngine.Time.time;
        }
        public virtual void LogicUpdate() {}

        public virtual void PhysicsUpdate(){}


        public virtual void Exit(){

        }

        // public abstract void  CanChangeState(ActionRequestData actionRequestData);

        public virtual void SetMovementTarget(Vector2 position)
        {
            
        }

        public virtual void AnticipateState(ActionRequestData position)
        {
            
        }
    }
}
   