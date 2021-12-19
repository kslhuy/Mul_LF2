using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerStateMachine 
    {
        public State[] states;
        public StateType CurrentState{ get ; private set;}


        public PlayerStateMachine(){
            int numberStates = System.Enum.GetNames(typeof(StateType)).Length;
            states =  new State[numberStates];
        }

        // RegisterState , instantiated in the first time 
        public void RegisterState(State state){
            int index = (int)state.GetId();
            states[index] = state;
        }

        // Do convert enum StateType == > State corresponse 
        public State GetState (StateType stateType){
            int index = (int)stateType;
            return states[index];
        }

        public void Update() {
            GetState(CurrentState)?.LogicUpdate();
            if(CurrentState != StateType.Idle && CurrentState != StateType.Move  ){
                SkillsDescription skillsDescription = GetState(CurrentState).SkillDescription(CurrentState);
                if (skillsDescription.expirable ){
                    var timeElapsed = Time.time - GetState(CurrentState).TimeStarted;
                    bool timeExpired =  timeElapsed >= skillsDescription.DurationSeconds ;
                    if (timeExpired){
                        GetState(CurrentState)?.End();
                    }
                }
            }
        }


        public void ChangeState(StateType newState ){
            GetState(CurrentState)?.Exit();
            CurrentState = newState;
            GetState(CurrentState)?.Enter();
        }

        public void SetMovementDirection(Vector2 targetPosition)
        {
            GetState(CurrentState).SetMovementTarget( targetPosition);
        }

        public void PhysicUpdate()
        {
            GetState(CurrentState)?.PhysicsUpdate();
        }

        // Note Only 3 basics State (Attack , Jump , Defense )  and Skills (DDA , DDJ ...) can be checked 
        public void RequestChangeState(StateRequestData data){

            GetState(CurrentState).CanChangeState(data);
        }


        /// <summary>
        /// Tells all active Actions that a particular gameplay event happened, such as being hit,
        /// getting healed, dying, etc. Actions can change their behavior as a result.
        /// </summary>
        /// <param name="activityThatOccurred">The type of event that has occurred</param>
        public void OnGameplayActivity(State.GameplayActivity activityThatOccurred)
        {
            if (activityThatOccurred == State.GameplayActivity.AttackedByEnemy){
                // GetState(CurrentState).OnGameplayActivity(activityThatOccurred);
                ChangeState(StateType.Hurt);
                }


        }

    }
}
