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
            // Debug.Log();
            // if (){
            //     m_Parent.OurAnimator.Play("Land_anim");
                
            // }
            Debug.Log("Jump");
            return ActionConclusion.Continue;
        }

        
        public override void AnticipateAction()
        {
            base.AnticipateAction();

            //note: because the hit-react is driven from the animation, this means we can anticipatively trigger a hit-react too. That
            //will make combat feel responsive, but of course the actual damage won't be applied until the server tells us about it.
            PlayAnim();
        }
    }
}