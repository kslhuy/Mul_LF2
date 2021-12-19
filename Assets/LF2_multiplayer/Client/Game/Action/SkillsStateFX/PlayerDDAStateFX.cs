
namespace LF2.Visual{
    

    public class PlayerDDAStateFX : StateFX
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;
        private bool m_ImpactPlayed;

        public PlayerDDAStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            base.Enter();
        }


        public override StateType GetId()
        {
            return StateType.DDA;
        }

        public override bool LogicUpdate()
        {
            // Debug.Log("Attack Visual");
            return true;
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }


        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("DDA_anim");
        }

    }
}
