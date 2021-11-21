using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    public class PlayerStateFX
    {

        public PlayerStateMachineFX stateMachineViz;

        
        public ClientCharacterVisualization m_ClientVisual { get; private set; }

        public PlayerStateFX(ClientCharacterVisualization parent)
        {
            m_ClientVisual = parent;
            stateMachineViz = new PlayerStateMachineFX();
            stateMachineViz.RegisterState(new PlayerIdleStateFX(this));
            stateMachineViz.RegisterState(new PlayerMoveStateFX(this));
            stateMachineViz.RegisterState(new PlayerJumpStateFX(this));
            stateMachineViz.RegisterState(new PlayerAttackStateFX(this));
            stateMachineViz.RegisterState(new PlayerLandStateFX(this));

            
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
