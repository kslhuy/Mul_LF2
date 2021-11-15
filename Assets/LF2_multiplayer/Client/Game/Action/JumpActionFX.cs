using MLAPI;
using MLAPI.Spawning;
// using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual
{
    public class JumpActionFX : ActionFX
    {
        public JumpActionFX(ref ActionRequestData data, ClientCharacterVisualization parent) : base(ref data, parent)
        {
        }

        public override bool Start()
        {
            if(!Anticipated)
            {
                PlayAnim();
            }

            base.Start();


            return true;
        }

        private void PlayAnim()
        {
            m_Parent.OurAnimator.Play("Jump_anim");
        }

        public override bool Update()
        {
            
            Debug.Log("JumpANIMATION");
            return ActionConclusion.Continue;
        }

        public override void End(){
            m_Parent.OurAnimator.Play("Land_anim");
        }

        public override void Cancel(){
            m_Parent.OurAnimator.Play("Land_anim");
        }

        
        public override void AnticipateAction()
        {
            base.AnticipateAction();
            PlayAnim();
        }
    }
}