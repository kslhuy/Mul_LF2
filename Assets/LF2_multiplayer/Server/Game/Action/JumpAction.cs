using UnityEngine;

namespace LF2.Server
{
    public class JumpAction : Action
    {
        private ServerCharacterMovement m_Movement;

        public JumpAction(ServerCharacter parent, ref ActionRequestData data) : base(parent, ref data)
        {
        }

        public override bool Start()
        {
            m_Movement = m_Parent.GetComponent<ServerCharacterMovement>();
            m_Movement.SetJump(m_Data.Direction);
            return true;
        }

        public override bool Update()
        {
            m_Movement.SetFallingDown();

            return true;
        }
    }
}