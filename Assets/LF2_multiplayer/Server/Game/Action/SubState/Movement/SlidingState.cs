using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  LF2.Server{

    public class SlidingState : State
    {
        private float _runSpeed;
        private float _gainDecreaseRunSpeed;

        public SlidingState(CharacterTypeEnum characterType, PlayerState player) : base(characterType, player)
        {
        }

        //     _runSpeed =playerData.runVelocity;
        // _gainDecreaseRunSpeed = playerData.GainDecreaseRunSpeed;

        public override void CanChangeState(StateRequestData actionRequestData){
            
        }

        public override void LogicUpdate()
        {
            // _runSpeed -= _runSpeed*Time.deltaTime*_gainDecreaseRunSpeed;
            // core.SetMovement.SetVelocitySliding(_gainDecreaseRunSpeed , _runSpeed);
            
            // if (_runSpeed <= 0.1f ){
            //     stateMachine.ChangeState(player.IdleState);
            // }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            ResetRunVelocity();
        }

        public void ResetRunVelocity(){
            // _runSpeed = playerData.runVelocity;
        }

        public override StateType GetId(){
            return StateType.Sliding;
        }


    }
}