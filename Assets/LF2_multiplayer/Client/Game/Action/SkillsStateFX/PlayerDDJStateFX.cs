using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerDDJStateFX : StateFX
    {
        private List<ClientDamageReceiver> AllTargets = new List<ClientDamageReceiver>();
        public PlayerDDJStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(StateType.DDJ);
            }
            base.Enter();
        }

        public override StateType GetId()
        {
            return StateType.DDJ;
        }

        public override void LogicUpdate()
        {        
            MPlayerMachineFX.CoreMovement.SetRunORRoll(2f);
        }

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
            MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("DDJ_1_anim");
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
                    m_data.Direction = new Vector3 (MPlayerMachineFX.CoreMovement.FacingDirection*1f,0,0);
                    targetClient.ChildVizObject.MStateMachinePlayerViz.CoreMovement.SetJump(m_data.Direction);
                    // clientChar.ChildVizObject.MStateMachinePlayerViz.PerformActionFX(ref m_data);
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
