using UnityEngine;

namespace LF2.Server
{
    internal class LandAction : Action
    {
        public LandAction(ServerCharacter parent, ref ActionRequestData data) : base(parent, ref data)
        {
        }

        public override bool Start()
        {
            return true;
        }

        public override bool Update()
        {
            Debug.Log("Land");
            return true;
        }
    }
}