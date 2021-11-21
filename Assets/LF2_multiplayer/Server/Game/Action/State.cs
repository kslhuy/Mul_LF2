using System;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server
{
    public abstract class State : StateBase
    {
        protected PlayerState player;

        protected Vector3 workSpace;
        protected MovementState currentMovementState;
        
        protected  SetMovement setMovement;

        protected ActionRequestData m_ActionRequestData;

        public bool IsMove { get; private set; }

        /// <summary>
        /// constructor. The "data" parameter should not be retained after passing in to this method, because we take ownership of its internal memory.
        /// </summary>
        public State( PlayerState player , SetMovement setMovement) 
        {
            this.player = player;
            this.setMovement = setMovement;
           
        }

        public abstract StateType GetId();
        public virtual void Enter(){

        }
        public virtual void LogicUpdate() {}

        public virtual void PhysicsUpdate(){}
   

        public virtual void Exit(){

        }

        public abstract void  CanChangeState(ActionRequestData actionRequestData);

        public virtual void SetMovementTarget(Vector2 position)
        {
            workSpace.Set(position.x , 0, position.y);
             
            // workSpace = position;
            IsMove  = position.x != 0 || position.y != 0;
        }
    }
}