using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace LF2.Visual{

    public class PlayerHurtStateFX : StateFX
    {
        public PlayerHurtStateFX(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }

        public override void Enter()
        {
            if( !Anticipated)
            {
                PlayAnim(m_PlayerFX.stateMachineViz.CurrentStateViz , Data.NbAnimation) ;
            }            
            base.Enter();
            if (Data.Direction != Vector3.zero){
                m_PlayerFX.m_ClientVisual.coreMovement.SetJump(new Vector3(0,0.5f,0));
            }
        }


        public override void Exit()
        {
            base.Exit();
        }


        public override bool LogicUpdate()
        {
            return true;
        }


        public override void End()
        {
            m_PlayerFX.stateMachineViz.ChangeState(StateType.Idle);
        }

        public override void PlayAnim(StateType currentState , int nbanim )
        {
            base.PlayAnim(currentState,nbanim);
            // Debug.Log(" AnimationAttack");
            Debug.Log(nbanim);
            // m_PlayerFX.m_ClientVisual.OurAnimator.Play("Hurt1_anim");


            switch (nbanim){
                case 1 : 
                    m_PlayerFX.m_ClientVisual.OurAnimator.Play("Hurt1_anim");
                    break;
                case 2 : 
                    m_PlayerFX.m_ClientVisual.OurAnimator.Play("Hurt2_anim");
                    break;
                case 3 : 
                    m_PlayerFX.m_ClientVisual.OurAnimator.Play("Attack3_anim");
                    break;

            } 
        }

        public override StateType GetId()
        {
            return StateType.Hurt;
        }


    }
 }
