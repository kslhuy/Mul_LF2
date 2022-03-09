
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerDUAStateFX : StateFX
    {
 
        private List<ClientDamageReceiver> AllTargets = new List<ClientDamageReceiver>();

        public PlayerDUAStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(StateType.DUA);
            }
            base.Enter();
        }




        public override void LogicUpdate()
        {    }


        public override void Exit()
        {
            AllTargets = new List<ClientDamageReceiver>();
        }

        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType currentState , int nbanim = 0)
        {
            base.PlayAnim(currentState);
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("DUA_1_anim");
        }
        public override StateType GetId()
        {
            return StateType.DUA;
        }

        public override void OnAnimEvent(string id)
        {
            PlayHitReact(); // If collide with enemy
        }

        private void PlayHitReact()
        {
            foreach (ClientDamageReceiver targetClient in AllTargets){
                if (targetClient.NetworkObjectId != MPlayerMachineFX.m_ClientVisual.NetworkObjectId){
                    
                    StateRequestData m_data = new StateRequestData();
                    m_data.StateTypeEnum = StateType.Fall;
                    // Test : Need change
                    m_data.Direction = new Vector3 (0,1.5f,0);
                    targetClient.ChildVizObject.MStateMachinePlayerViz.CoreMovement.SetJump(m_data.Direction);
                    targetClient.ReceiveHP(m_data,-MPlayerMachineFX.SkillDescription(GetId()).Amount);
                }
            }
        }
        public override void AddCollider(Collider collider)
        {
            ClientDamageReceiver targetClientChar = collider.GetComponentInParent<ClientDamageReceiver>();
            if (targetClientChar != null){
                AllTargets.Add(targetClientChar);
            }
        }

        public override void RemoveCollider(Collider collider)
        {
            ClientDamageReceiver targetClientChar = collider.GetComponentInParent<ClientDamageReceiver>();
            if (targetClientChar != null){
                AllTargets.Remove(targetClientChar);
            }
        }

    }
}
