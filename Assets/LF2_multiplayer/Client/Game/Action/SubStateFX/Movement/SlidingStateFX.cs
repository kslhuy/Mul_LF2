using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  LF2.Visual{

    public class SlidingState : StateFX
    {
        private float _runSpeed;
        private float _gainDecreaseRunSpeed;

        public SlidingState(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        //     _runSpeed =playerData.runVelocity;
        // _gainDecreaseRunSpeed = playerData.GainDecreaseRunSpeed;



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



        public override void Enter()
        {
            base.Enter();
        }

        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }

        public override void AnticipateState(ref StateRequestData requestData)
        {
            base.AnticipateState(ref requestData);
        }

        public override void End()
        {
            base.End();
        }

        public override bool LogicUpdate()
        {
            return true;
        }
    }
}