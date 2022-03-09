using UnityEngine;

namespace LF2.Server{

    class davisMove : Move
    {
        public davisMove(AIBrain aIBrain) : base(aIBrain)
        {
        }

        public override void FindTheNextState()
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
            
            Vector3 pos = m_Brain.GetMyServerCharacter().physicsWrapper.Transform.position;
            var target =  m_Brain.searchClosestTarget(pos);
            
            // Move to target

            Vector3 targetDir = (target.physicsWrapper.Transform.position - pos).normalized ; 
            
            m_Brain.Movement.FollowTarget(targetDir);

        }

        

    }
}