using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    /// <summary>
    /// This is a companion class to ClientCharacterVisualization that is specifically responsible for visualizing State. 
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
            stateMachineViz.RegisterState(new PlayerJumpStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerAttackStateFX(characterType,this));
            stateMachineViz.RegisterState(new PlayerLandStateFX(characterType,this));

            // Intiliazie State
            stateMachineViz.ChangeState(StateType.Idle);

        }



        public void Update() {
            stateMachineViz.Update();
        }

        // Aticipate State in CLient , Just run Animation , So not run Update () 
        public void AnticipateState(ref StateRequestData data)
        {
            stateMachineViz.GetState(stateMachineViz.CurrentStateViz).AnticipateState(data);
        }

        // Play correct State that sent by Server
        public void PlayState(ref StateRequestData data)
        {
            stateMachineViz.GetState(stateMachineViz.CurrentStateViz).Enter();
        }


        public void OnMoveInput(Vector2 position)
        {
            stateMachineViz.OnMoveInput( position);
        }
    }
}