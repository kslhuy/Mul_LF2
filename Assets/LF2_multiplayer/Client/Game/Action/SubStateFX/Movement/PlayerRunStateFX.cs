using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LF2.Visual{

    public class PlayerRunStateFX : StateFX
    {
        private float runVelocity ;

        public PlayerRunStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void AnticipateState(ref StateRequestData requestData)
        {

            if (requestData.StateTypeEnum == StateType.Jump || requestData.StateTypeEnum == StateType.Defense || requestData.StateTypeEnum == StateType.Attack ){
                MPlayerMachineFX.GetState(requestData.StateTypeEnum).PlayAnim(requestData.StateTypeEnum);

                // MPlayerMachineFX.ChangeState(new PlayerDoubleJumpStateFX(MPlayerMachineFX));
            }
            else if (requestData.StateTypeEnum == StateType.Defense){
                // MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Rolling_anim");
                MPlayerMachineFX.GetState(StateType.Defense).PlayAnim(StateType.Rolling);

            }
            else if (requestData.StateTypeEnum == StateType.Attack)
            {
                MPlayerMachineFX.GetState(StateType.Attack).PlayAnim(StateType.Attack);
            }           
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Run_anim");
            }
            base.Enter();

        }


        public override void Exit()
        {
            base.Exit();
        }

        public override StateType GetId()
        {
            return StateType.Run;
        }



        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);

        }



        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Run_anim");
        }


        public override void LogicUpdate()
        {
            MPlayerMachineFX.m_ClientVisual.coreMovement.SetRunORRoll(2f);
            if ( MPlayerMachineFX.moveDir.z >0.9f || MPlayerMachineFX.moveDir.z < -0.9f  ){
                MPlayerMachineFX.ChangeState(StateType.Sliding);
            }

        }
    }
}
