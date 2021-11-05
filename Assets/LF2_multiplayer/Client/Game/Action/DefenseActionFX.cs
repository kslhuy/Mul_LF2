using System;

namespace LF2.Visual
{
    public class DefenseActionFX : ActionFX
    {
        private bool m_ImpactPlayed;

        public DefenseActionFX(ref ActionRequestData data, ClientCharacterVisualization parent) : base(ref data, parent)
        {
        }

        public override bool Start()
        {
            if( !Anticipated)
            {
                PlayAnim();
            }

            base.Start();

            return true;
        }

        private void PlayAnim()
        {
            m_Parent.OurAnimator.Play("Defense_anim");
        }

        public override void AnticipateAction()
        {
            base.AnticipateAction();

            //note: because the hit-react is driven from the animation, this means we can anticipatively trigger a hit-react too. That
            //will make combat feel responsive, but of course the actual damage won't be applied until the server tells us about it.
            PlayAnim();
        }

        public override void Cancel()
        {
            base.Cancel();
        }

        public override void End()
        {
            PlayHitReact();
            base.End();
        }
        private void PlayHitReact()
        {
            // if (m_ImpactPlayed) { return; }
            // m_ImpactPlayed = true;

            // //Is my original target still in range? Then definitely get him!
            // if (Data.TargetIds != null && Data.TargetIds.Length > 0 && NetworkSpawnManager.SpawnedObjects.ContainsKey(Data.TargetIds[0]))
            // {
            //     NetworkObject originalTarget = NetworkSpawnManager.SpawnedObjects[Data.TargetIds[0]];
            //     float padRange = Description.Range + k_RangePadding;

            //     if ((m_Parent.transform.position - originalTarget.transform.position).sqrMagnitude < (padRange * padRange))
            //     {
            //         if( originalTarget.NetworkObjectId != m_Parent.NetworkObjectId )
            //         {
            //             string hitAnim = Description.ReactAnim;
            //             if(string.IsNullOrEmpty(hitAnim)) { hitAnim = k_DefaultHitReact; }
            //             var clientChar = originalTarget.GetComponent<Client.ClientCharacter>();
            //             if (clientChar && clientChar.ChildVizObject && clientChar.ChildVizObject.OurAnimator)
            //             {
            //                 clientChar.ChildVizObject.OurAnimator.SetTrigger(hitAnim);
            //             }
            //         }
            //     }
            // }

            //in the future we may do another physics check to handle the case where a target "ran under our weapon".
            //But for now, if the original target is no longer present, then we just don't play our hit react on anything.
        }


        public override void OnAnimEvent(string id)
        {
            if (id == "impact" && !m_ImpactPlayed)
            {
                PlayHitReact();
            }
        }

        public override void OnStoppedChargingUp(float finalChargeUpPercentage)
        {
            base.OnStoppedChargingUp(finalChargeUpPercentage);
        }




        public override bool Update()
        {
            return ActionConclusion.Continue;
        }
    }
}