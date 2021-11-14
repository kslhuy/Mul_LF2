namespace LF2.Server
{
    public class DefenseAction : Action
    {
        public DefenseAction(ServerCharacter parent, ref ActionRequestData data) : base(parent, ref data)
        {
        }

        public override bool Start()
        {
            return true;
        }

        public override bool Update()
        {
            return true;
        }
    }
}