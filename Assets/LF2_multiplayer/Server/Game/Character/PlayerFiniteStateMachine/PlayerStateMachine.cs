using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    
    public class PlayerStateMachine : stateMachineBase
    {
        public ServerCharacter serverplayer;
        public ServerCharacterMovement ServerCharacterMovement;

        public Vector3 moveDir;


        
        public State[] states = new State[Enum.GetNames(typeof(StateType)).Length];
        private SkillsDescription skillsDescription;

        public State CurrentState{ get ; private set;}
        private State m_lastState;



    

        public PlayerStateMachine(ServerCharacter serverplayer
                                ,CharacterTypeEnum characterType) : base(characterType)
        {
            this.serverplayer = serverplayer;
            ServerCharacterMovement = serverplayer.Movement;

            RegisterState(new PlayerIdleState(this ));
            RegisterState(new PlayerMoveState(this ));
            RegisterState(new PlayerRunState(this));
            RegisterState(new SlidingState(this));
            RegisterState(new PlayerRollingState(this));

            RegisterState(new PlayerJumpState(this ));
            RegisterState(new PlayerDoubleJumpState(this ));
            

            RegisterState(new PlayerLandState(this ));

            RegisterState(new PlayerAttackState(this ));
            RegisterState(new PlayerAttackJump1(this ));
            RegisterState(new PlayerDefenseState(this));

            RegisterState(new PlayerHurtState(this));
            RegisterState(new PlayerFallState(this));




            RegisterState(new PlayerDDAState(this));
            RegisterState(new PlayerDDJState(this));
            RegisterState(new PlayerDUAState(this));
            RegisterState(new PlayerDUJState(this));


            CurrentState = GetState(StateType.Idle);
    }



        // Client send input to Server to change the state of player
        public void RequestToState(ref StateRequestData requestData)
        {
            // Get data resquest to state correspond
            GetState(requestData.StateTypeEnum).m_Data = requestData;
            RequestChangeState(requestData);
        }

        // Client request to Server to Move the state of player

        // Something happen in the game 
        // Used to change State passively (tu chuyen doi state khi gap mot su kien nao do)
        // Exemple : Hurt (Attack by someone) change state player in Server to HurtState

        

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
            Debug.Log(CurrentState);
            if (CurrentState.GetId() == StateType.Idle) return;

            if (CurrentState.GetId() == StateType.Move){
                m_lastState = CurrentState;
                CurrentState.LogicUpdate();
                return;
            }

            if ( m_lastState != CurrentState){
                m_lastState = CurrentState;
                if (CurrentState.GetId() == StateType.Idle || CurrentState.GetId() == StateType.Move ) return;
                skillsDescription =  SkillDescription(CurrentState.GetId()); // Get All Skills Data of actual Player Charater we current play.
            } 

            if (skillsDescription!= null){
                if (skillsDescription.expirable)
                {
                    // Debug.Log($"Sub_TimeAnimation = {Time.time -  CurrentState.TimeStarted_Animation} "); 
                    bool timeExpired = Time.time -  CurrentState.TimeStarted_Server >= skillsDescription.DurationSeconds;

                    // Check if this State Can End Naturally (== time Expired )
                    if ( timeExpired ){
                        CurrentState?.End();
                        return;
                    }
                }
            }
            CurrentState.LogicUpdate();

        }


        public void ChangeState(StateType newState ){
            CurrentState.Exit();
            CurrentState = GetState(newState);
            CurrentState.Enter();
        }

        public void SetMovementDirection(Vector2 moveXZ)
        {
            moveDir.Set(moveXZ.x , 0, moveXZ.y);   
            CurrentState.SetMovementDir( moveXZ);
        }

        // Note Only 3 basics StateType (Attack , Jump , Defense )  and Skills (DDA , DDJ ...) can be checked 
        public void RequestChangeState(StateRequestData data){

            CurrentState.CanChangeState(data);
        }

        // TO DO : NEED To change logic 
        /// <summary>
        /// Tells all active Actions that a particular gameplay event happened, such as being hit,
        /// getting healed, dying, etc. Actions can change their behavior as a result.
        /// </summary>
        /// <param name="activityThatOccurred">The type of event that has occurred</param>
        public void OnGameplayActivity(StateRequestData stateRequestData)
        {
            // if (CurrentState.GetId() == stateRequestData.StateTypeEnum) return; 
            CurrentState.Exit();
            CurrentState = GetState(stateRequestData.StateTypeEnum);
            CurrentState.Enter();
        }

    }
    
}
