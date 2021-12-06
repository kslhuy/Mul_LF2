// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// namespace LF2.Server{


//     public class AIState 
//     {
//         // protected FiniteStateMachine stateMachine;
//         protected ServerCharacter player;

//         public int HashIDanimName { get; }

//         protected float startTime;

//         protected AIBrainNew aIBrain;

//         // protected bool isAnimationFinished;

        

//         public AIState(AIBrainNew aIBrain , ServerCharacter player){

//             this.aIBrain = aIBrain;
//             this.player = player;
//             // HashIDanimName = IDanim;
//         }

//         public virtual void Enter(){
//             // startTime = Time.deltaTime;
//             // player.AnimationBase.Anim.Play(HashIDanimName);

//         }
//         public virtual void LogicUpdate(){}
        
//         public virtual void PhysicsUpdate(){}
        
//         // public virtual void Exit(){}

//         // public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

//         // protected bool isFinishedAnimation()  {
//         //     return entity.AnimationBase.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;}


        
//     }
// }
