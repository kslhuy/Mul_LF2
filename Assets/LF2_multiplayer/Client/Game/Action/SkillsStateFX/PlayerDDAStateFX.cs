
namespace LF2.Visual{
    

    public class PlayerDDAStateFX : StateFX
    {

        float attack12distance;
        private bool m_ImpactPlayed;

        public PlayerDDAStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(StateType.DDA);
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.DDA;
        }

        public override void LogicUpdate()
        {        }


        public override void Exit()
        {
            base.Exit();
        }

        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("DDA_1_anim");
        }

    }
}
