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


            return true;
        }

        private void PlayAnim()
        {
            m_Parent.OurAnimator.SetTrigger(Description.Anim);
        }

        public override bool Update()
        {
            return true;
        }
    }
}