using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackStateFX : StateFX
    {
        // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
        // Transform attackTransform ;
        float attack12distance;
        private bool m_ImpactPlayed;

        public PlayerAttackStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }


        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz);
            }
            base.Enter();
            // PlayHitReact();
        }


        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override bool LogicUpdate()
        {
            // Debug.Log("Attack Visual");
            return true;
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override void End(){
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }


        public override void PlayAnim(StateType currentState)
        {
            base.PlayAnim(currentState);
            m_PlayerFX.m_ClientVisual.OurAnimator.Play("Attack1_anim");
        }



        // private void PlayHitReact()
        // {
        //     // if (m_ImpactPlayed) { return; }
        //     // m_ImpactPlayed = true;
        //     // Debug.Log("PlayHitReact");
        //     Debug.Log(Data);
        //     //Is my original target still in range? Then definitely get him!
        //     if (Data.TargetIds != null && Data.TargetIds.Length > 0 && NetworkSpawnManager.SpawnedObjects.ContainsKey(Data.TargetIds[0]))
        //     {
        //         NetworkObject originalTarget = NetworkSpawnManager.SpawnedObjects[Data.TargetIds[0]];
  
        //         if( originalTarget.NetworkObjectId != m_PlayerFX.m_ClientVisual.NetworkObjectId )
        //         {
        //             // string hitAnim = Description.ReactAnim;
        //             // if(string.IsNullOrEmpty(hitAnim)) { hitAnim = k_DefaultHitReact; }
        //             var clientChar = originalTarget.GetComponent<Client.ClientCharacter>();
        //             // Debug.Log(originalTarget);

        //             if (clientChar && clientChar.ChildVizObject && clientChar.ChildVizObject.OurAnimator)
        //             {
        //                 clientChar.ChildVizObject.OurAnimator.Play("Hurt1_anim");
        //             }
        //         }

        //     }

            //in the future we may do another physics check to handle the case where a target "ran under our weapon".
            //But for now, if the original target is no longer present, then we just don't play our hit react on anything.
        }
    
}
