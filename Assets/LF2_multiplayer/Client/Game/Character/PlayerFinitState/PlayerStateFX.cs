using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    /// <summary>
    /// This is a companion class to ClientCharacterVisualization that is specifically responsible for visualizing State. 

    //     
    /// The flow for Visual is:
    /// Initially: Aniticipate() only play action 
    /// if recevie signal from Server , call PlayState() to active  LogicUpdate() + PhysicUpdate() 
  


    /// </summary>
    public class PlayerStateFX
    {

        public PlayerStateMachineFX stateMachineViz;

        
        public ClientCharacterVisualization m_ClientVisual { get; private set; }

        public PlayerStateFX(ClientCharacterVisualization parent , CharacterTypeEnum characterType)
        {
            m_ClientVisual = parent;
            stateMachineViz = new PlayerStateMachineFX();
            stateMachineViz.RegisterState(new PlayerIdleStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerMoveStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerRunStateFX(characterType,this));


            stateMachineViz.RegisterState(new PlayerJumpStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerDoubleJumpStateFX(characterType,this));



            stateMachineViz.RegisterState(new PlayerLandStateFX(characterType,this));

            stateMachineViz.RegisterState(new PlayerAttackStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerAttackJump1FX(characterType,this));

            stateMachineViz.RegisterState(new PlayerDefenseStateFX(characterType,this));

            stateMachineViz.RegisterState(new PlayerHurtStateFX(characterType,this));

            stateMachineViz.RegisterState(new PlayerDDAStateFX(characterType,this));


            // Intiliazie State
            stateMachineViz.ChangeState(StateType.Idle);

        }



        public void Update() {
            stateMachineViz.Update();
        }

        // Aticipate State in CLient , Just run Animation , So not run Update () 
        public void AnticipateState(ref StateRequestData data)
        {
            stateMachineViz.GetState(stateMachineViz.CurrentStateViz).AnticipateState(ref data);
        }

        // Play correct State that sent by Server 
        public void PerformActionFX(ref StateRequestData data)
        {
            stateMachineViz.GetState(data.StateTypeEnum).Data = data;
            stateMachineViz.ChangeState(data.StateTypeEnum);
        }


        public void OnMoveInput(Vector2 position)
        {
            stateMachineViz.OnMoveInput(position);
        }

        public void OnAnimEvent(string id)
        {
            stateMachineViz.OnAnimEvent(id);
        }
    }
}
