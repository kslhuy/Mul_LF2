using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerState 
    {
        protected ServerCharacter player;
        protected PlayerStateMachine stateMachine;



        protected bool isAnimationFinished;

        protected float startTime;

        
        public PlayerState(ServerCharacter player , PlayerStateMachine stateMachine ){
            this.player = player;
            this.stateMachine = stateMachine;
        }



        public virtual void Enter(){
            startTime = Time.time;
            isAnimationFinished = false;
        }

        public virtual void Exit(){}

        public virtual void LogicUpdate() {

        }

        public virtual void PhysicsUpdate(){}

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
        

        // protected bool isFinishedAnimation(){
        //     return player.AnimationBase.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;}

    }
}
