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
            stateMachine.RegisterState(new PlayerJumpState(chacterType,this ));
            stateMachine.RegisterState(new PlayerDoubleJumpState(chacterType,this ));
            

            stateMachine.RegisterState(new PlayerLandState(chacterType,this ));

            stateMachine.RegisterState(new PlayerAttackState(chacterType,this ));
            stateMachine.RegisterState(new PlayerAttackJump1(chacterType,this ));
            stateMachine.RegisterState(new PlayerDefenseState(chacterType,this));


            stateMachine.RegisterState(new PlayerDDAState(chacterType,this));

            stateMachine.ChangeState(StateType.Idle);

            

        }

        public void Update() {
            stateMachine.Update();
        }

        public void PhysicsUpdate(){
            stateMachine.PhysicUpdate();
        }

        public void RequestToState(ref StateRequestData action)
        {
            stateMachine.RequestChangeState(action);
        }

        public void SetMovementDirection(Vector2 targetPosition)
        {
            stateMachine.SetMovementDirection(targetPosition);
        }

        public void OnGameplayActivity(State.GameplayActivity activityThatOccurred){
            stateMachine.OnGameplayActivity(activityThatOccurred);
        }
    }
}
