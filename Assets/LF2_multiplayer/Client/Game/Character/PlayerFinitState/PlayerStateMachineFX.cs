using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    public class PlayerStateMachineFX 
    {
        public StateFX[] statesViz;
        public StateType CurrentStateViz{ get ; private set;}

        public Dictionary<StateType , StateFX> m_logic;


        public PlayerStateMachineFX(){
            int numberStates = System.Enum.GetNames(typeof(StateType)).Length;
            statesViz =  new StateFX[numberStates];


        }

        // RegisterState , instantiated in the first time 
        public void RegisterState(StateFX state){
            int index = (int)state.GetId();
            statesViz[index] = state;
        }


        // Do convert enum StateType == > State corresponse 
        public StateFX GetState (StateType stateType){
            int index = (int)stateType;
            return statesViz[index];
        }

        public void Update() {
            GetState(CurrentStateViz)?.LogicUpdate();
        }


        public void ChangeState(StateType newState){
            GetState(CurrentStateViz)?.Exit();
            CurrentStateViz = newState;
            GetState(CurrentStateViz)?.Enter();
        }


        public void OnMoveInput(Vector2 position)
        {
            GetState(CurrentStateViz).SetMovementTarget(position);
        }

        // public void AnticipateState(ActionRequestData data)
        // {
        //     GetState(CurrentStateViz).AnticipateState(data);
        // }
    }
}
