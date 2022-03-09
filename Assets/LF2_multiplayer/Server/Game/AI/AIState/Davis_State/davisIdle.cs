using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server
{
    public class davisIdle : Idle
    {
        private AIBrain davisBrain;

        public davisIdle(AIBrain aIBrain) : base(aIBrain)
        {
            davisBrain = aIBrain;
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
            base.Update();
            FindTheNextState();
        }
        
        public override void FindTheNextState()
        {
            // Change state to chase (move)
            if (davisBrain.GetHatedEnemies().Count != 0)  davisBrain.ChangeState(AIStateType.MOVE); 
        }

    }
}
