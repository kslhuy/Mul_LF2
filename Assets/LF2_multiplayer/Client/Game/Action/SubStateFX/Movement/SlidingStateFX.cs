using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  LF2.Visual{

    public class SlidingStateFX : StateFX
    {
        private float _runSpeed;
        private float _gainDecreaseRunSpeed;

        public SlidingStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
            _runSpeed = mPlayerMachineFX.m_ClientVisual.m_NetState.CharacterClass.Speed;
            _gainDecreaseRunSpeed = 4f;
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Sliding_anim");    
            }
            base.Enter();
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Sliding_anim");  
            base.PlayAnim(currentState, nbAniamtion);

        }


        public override void LogicUpdate()
        {
            _runSpeed -= _gainDecreaseRunSpeed*Time.deltaTime;
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetRunORRoll(_runSpeed);

            if (_runSpeed < 0){
                MPlayerMachineFX.GetState(StateType.Idle).PlayAnim(StateType.Idle);
            }
        }

        
        public override void Exit()
        {
            base.Exit();
            ResetRunVelocity();
        }

        public override StateType GetId(){
            return StateType.Sliding;
        }


        
        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }


        public void ResetRunVelocity(){
            _runSpeed = MPlayerMachineFX.m_ClientVisual.m_NetState.CharacterClass.Speed;
        }


    }
}