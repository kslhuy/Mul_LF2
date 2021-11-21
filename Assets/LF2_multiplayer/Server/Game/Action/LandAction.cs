using UnityEngine;

namespace LF2.Server
{
    public class LandAction : Action
    {
        public LandAction(ServerCharacter parent, ref ActionRequestData data) : base(parent, ref data)
        {
        }

        public override bool Start()
        {
            m_Parent.NetState.RecvDoActionClientRPC(Data);

            return true;
        }

        public override bool Update()
        {
            Debug.Log("Land");
            return true;
        }



    }
}