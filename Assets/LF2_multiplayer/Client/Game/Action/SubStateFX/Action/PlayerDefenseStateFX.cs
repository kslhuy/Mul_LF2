using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerDefenseStateFX : StateFX
    {

        public PlayerDefenseStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }

        public override void AnticipateState(ref StateRequestData data)
        {
            if (data.StateTypeEnum == StateType.DDA ||
                data.StateTypeEnum == StateType.DDJ||
                data.StateTypeEnum == StateType.DUJ||
                data.StateTypeEnum == StateType.DUA){
                MPlayerMachineFX.GetState(data.StateTypeEnum).PlayAnim(data.StateTypeEnum, data.NbAnimation);
            }


        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Defense_anim");
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.Defense;
        }

        public override void LogicUpdate()
        {        }


        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType state , int nbanim = 0)
        {
            base.PlayAnim(state);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Defense_anim");
        }
    }
}
