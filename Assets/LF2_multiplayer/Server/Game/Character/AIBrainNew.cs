using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace LF2.Server{

    public class AIBrainNew 
    {
        #region AI States 
        public EIdleState EIdleState {get ; private set;}
        public EMoveState EMoveState {get ; private set;}
        public ERunState ERunState {get ; private set;}

        // public EAttackState EAttackState{get ; private set;}
        // public EHurtState EHurtState{get ; private set;}
        // public EDeadState EDeadState{get ; private set;}

        // public ESlidingState ESlidingState{get ; private set;}
        
        #endregion
        public AIState currentState{get; private set;}

    
        public AIBrainNew(ServerCharacter player){

            EIdleState = new EIdleState(this,player); 
            EMoveState= new EMoveState(this,player);
            ERunState = new ERunState(this,player);
            // ESlidingState =new ESlidingState(this,player); 

            // EAttackState = new EAttackState(this,player, player.Attack1);
            
            // EHurtState = new EHurtState(this,player, player.Hurt1);
            // EDeadState = new EDeadState(this,player, player.Hurt3Contre);

            Initalize(EIdleState);

            
            
        }

        public void ChangeAIState(AIState aIState){
            currentState = aIState;
        }

        private void Initalize(AIState startingState){
            currentState = startingState;
            currentState.Enter();
        }


    }
}

