using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    
    public class PlayerState 
    {
        public ServerCharacter serverplayer;

        public ServerCharacterMovement ServerCharacterMovement;
        public PlayerStateMachine stateMachine;


        protected bool isAnimationFinished;

        protected float startTime;


        public PlayerState(ServerCharacter serverplayer ,ServerCharacterMovement serverCharacterMovement  ){
            this.serverplayer = serverplayer;
            ServerCharacterMovement = serverCharacterMovement;
            stateMachine = new PlayerStateMachine();

            CharacterTypeEnum chacterType =  serverplayer.NetState.CharacterType;
            // Debug.Log(chacterType);
            stateMachine.RegisterState(new PlayerIdleState(chacterType, this ));
            stateMachine.RegisterState(new PlayerMoveState(chacterType,this ));
            stateMachine.RegisterState(new PlayerRunState(chacterType,this));

            stateMachine.RegisterState(new PlayerJumpState(chacterType,this ));
            stateMachine.RegisterState(new PlayerDoubleJumpState(chacterType,this ));
            

            stateMachine.RegisterState(new PlayerLandState(chacterType,this ));

            stateMachine.RegisterState(new PlayerAttackState(chacterType,this ));
            stateMachine.RegisterState(new PlayerAttackJump1(chacterType,this ));
            stateMachine.RegisterState(new PlayerDefenseState(chacterType,this));

            stateMachine.RegisterState(new PlayerHurtState(chacterType,this));



            stateMachine.RegisterState(new PlayerDDAState(chacterType,this));

            stateMachine.ChangeState(StateType.Idle);

            

        }

        public void Update() {
            stateMachine.Update();
        }

        public void PhysicsUpdate(){
            stateMachine.PhysicUpdate();
        }

        // Client send input to Server to change the state of player
        public void RequestToState(ref StateRequestData requestData)
        {
            // Get data resquest to state correspond
            stateMachine.GetState(requestData.StateTypeEnum).m_Data = requestData;
            stateMachine.RequestChangeState(requestData);
        }

        // Client request to Server to Move the state of player

        public void SetMovementDirection(Vector2 targetPosition)
        {
            stateMachine.SetMovementDirection(targetPosition);
        }
        // Something happen in the game 
        // Used to change State passively (tu chuyen doi state khi gap mot su kien nao do)
        // Exemple : Hurt (Attack by someone) change state player in Server to HurtState
        public void OnGameplayActivity(State.GameplayActivity activityThatOccurred){
            stateMachine.OnGameplayActivity(activityThatOccurred);
        }
    }
}
