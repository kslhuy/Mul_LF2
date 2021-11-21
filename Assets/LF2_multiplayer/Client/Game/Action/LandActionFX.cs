using System;
using MLAPI;
using MLAPI.Spawning;
// using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual
{
    public class LandActionFX : ActionFX
    {
        public LandActionFX(ref ActionRequestData data, ClientCharacterVisualization parent) : base(ref data, parent)
        {
        }

        public override bool Start()
        {
            if(!Anticipated)
            {
                PlayAnim();
            }

            base.Start();
            Debug.Log("landVisual");


            return true;
        }

        private void PlayAnim()
        {
            // m_Parent.OurAnimator.Play(AnimId);
        }

        public override bool Update()
        {
            Debug.Log("LandActionFX");
            return false;
        }
        public override void End()
        {
            m_Parent.OurAnimator.Play("Idle_anim");
        }
        public override void Cancel()
        {
            m_Parent.OurAnimator.Play("Idle_anim");
        }
    }
}