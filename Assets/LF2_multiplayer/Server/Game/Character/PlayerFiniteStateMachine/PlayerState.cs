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
            stateMachine.ChangeState(StateType.Idle);

            stateMachine.RegisterState(new PlayerIdleState(this, serverplayer.SetMovement ));
            stateMachine.RegisterState(new PlayerMoveState(this , serverplayer.SetMovement));
            stateMachine.RegisterState(new PlayerAttackState(this, serverplayer.SetMovement ));
            stateMachine.RegisterState(new PlayerJumpState(this , serverplayer.SetMovement));
            stateMachine.RegisterState(new PlayerLandState(this , serverplayer.SetMovement));

        }

        public void Update() {
            stateMachine.Update();
        }

        public void PhysicsUpdate(){
            stateMachine.PhysicUpdate();
        }

        public void RequestToState(ref ActionRequestData action)
        {
            stateMachine.RequestChangeState(action);
        }

        public void SetMovementDirection(Vector2 targetPosition)
        {
            stateMachine.SetMovementDirection(targetPosition);
        }
    }
}
