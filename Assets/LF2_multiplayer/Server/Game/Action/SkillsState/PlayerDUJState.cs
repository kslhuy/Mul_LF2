
namespace LF2.Server{

    public class PlayerDUJState : State
    {

        public PlayerDUJState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {      
            base.Enter();
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }


        public override StateType GetId()
        {
            return StateType.DUJ;
        }


        public override void End()
        {
            player.ChangeState(StateType.Idle);
        }

      



    }
}
