using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    

    public class PlayerAttackStateFX : StateFX
    {
        private List<ClientDamageReceiver> AllTargets = new List<ClientDamageReceiver>();
        float attack12distance;
        private bool m_ImpactPlayed;

        private ClientDamageReceiver clientChar;

        private SO_AttackDetails attackDetails;
        private List<SpecialFXGraphic> m_SpawnedGraphics = null;



        public PlayerAttackStateFX(PlayerStateMachineFX mPlayerMachineFX) : base(mPlayerMachineFX)
        {

        }

        public override void Enter()
        {
            if(!Anticipated)
            {
                PlayAnim(MPlayerMachineFX.CurrentStateViz.GetId() , Data.NbAnimation);
            }
            base.Enter();

            PlayHitReact();

        }


        public override StateType GetId()
        {
            return StateType.Attack;
        }

        public override void LogicUpdate()
        {        }


        public override void Exit()
        {
            AllTargets = new List<ClientDamageReceiver>();
        }

        public override void End(){
            MPlayerMachineFX.idle();
        }


        public override void PlayAnim(StateType state , int nbanim )
        {
            base.PlayAnim(state,nbanim);


            // DownCasting data 

            if (MPlayerMachineFX.SkillDescription(StateType.Attack).GetType() == typeof(SO_AttackDetails)){
                attackDetails =(SO_AttackDetails)MPlayerMachineFX.SkillDescription(StateType.Attack);
            }
            
            // reset nbanim (case some champ not have a triple attack (combo attack))
            if (attackDetails.AttackDetails.Length < 3 && nbanim == 3 ){
                nbanim = 1;
            }

            // run Animation
            if (nbanim < 3 ){
                int ramdom_anim = UnityEngine.Random.Range(1,3);
                switch (ramdom_anim) {
                default : 
                    MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Attack1_anim");
                    break;
                case 2 : 
                    MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("Attack2_anim");
                    break;
                }
            }
            else{
                MPlayerMachineFX.m_ClientVisual.OurAnimator.Play("AttackRun_anim");
            }
            
        }



        private void PlayHitReact()
        {
            if (Data.TargetIds != null && 
                Data.TargetIds.Length > 0 && 
                NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(Data.TargetIds[0], out var targetNetworkObj)
                && targetNetworkObj != null)
            {
                if (targetNetworkObj.NetworkObjectId != MPlayerMachineFX.m_ClientVisual.NetworkObjectId)
                {

                    clientChar = targetNetworkObj.GetComponent<ClientDamageReceiver>();

                    if (clientChar && clientChar.ChildVizObject && clientChar.ChildVizObject.OurAnimator)
                    {
                        // Dont have Owner Ship to call serverRPC (can ignore but dont know further) 
                        StateRequestData m_data = new StateRequestData();
                        m_data.StateTypeEnum = StateType.Hurt;
                        m_data.NbAnimation = Data.NbAnimation;
                    }
                        
                    m_SpawnedGraphics = InstantiateSpecialFXGraphics(targetNetworkObj.transform, true,GetId());

                }
            }
        }
    }
}
