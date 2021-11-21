using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

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

            
            stateMachineViz.ChangeState(StateType.Idle);

        }

        protected bool isAnimationFinished;

        protected float startTime;

        
        public PlayerStateFX(  ){

        }

        public void Update() {
            stateMachineViz.Update();
        }

        // public void RequestToState(ref ActionRequestData action)
        // {
        //     stateMachine.RequestChangeState(action);
        // }


        public void AnticipateState(ref ActionRequestData data)
        {
            stateMachineViz.GetState(stateMachineViz.CurrentStateViz).AnticipateState(data);
        }

        public void PlayState(ref ActionRequestData data)
        {
            stateMachineViz.GetState(stateMachineViz.CurrentStateViz).Enter();
        }


        public void OnMoveInput(Vector2 position)
        {
            stateMachineViz.OnMoveInput( position);
        }
    }
}
