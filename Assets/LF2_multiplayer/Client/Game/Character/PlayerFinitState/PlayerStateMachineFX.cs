using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    
    public class PlayerStateMachineFX 
    {
        public StateFX[] statesViz; // All State we declare 
        public StateType CurrentStateViz; // CurrentState visual we are 

        // private List<StateType> m_StateExpirable; // 


        public PlayerStateMachineFX(){
            int numberStates = System.Enum.GetNames(typeof(StateType)).Length;
            statesViz =  new StateFX[numberStates];
            // m_StateExpirable = new List<StateType>();

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
            // Check ALL State that have actual Action correspond ( See in Game Data Soucre Objet )
            if(CurrentStateViz != StateType.Idle && CurrentStateViz != StateType.Move && CurrentStateViz != StateType.Land ){
                
                SkillsDescription skillsDescription =  GetState(CurrentStateViz).SkillDescription(CurrentStateViz); // Get All Skills Data of actual Player Charater we current play.
                bool keepGoing = GetState(CurrentStateViz).Anticipated || GetState(CurrentStateViz).LogicUpdate(); // (Trick of || (or) )only call Update() on actions that are past anticipation , 
                bool timeExpired =  GetState(CurrentStateViz).TimeRunning >= skillsDescription.DurationSeconds ;
                // Check if this State Can End Naturally (Mean time Expired )
                if (!keepGoing || timeExpired ){
                    GetState(CurrentStateViz)?.End();
                }
            }
        }

        // Switch to Another State , (we force to Change State , so that mean this State may be not End naturally , be interruped by some logic  ) 
        public void ChangeState(StateType newState){
            GetState(CurrentStateViz)?.Exit();
            CurrentStateViz = newState;
            GetState(CurrentStateViz)?.Enter();
        }

        // Movement in Client 
        public void OnMoveInput(Vector2 position)
        {
            GetState(CurrentStateViz).SetMovementTarget(position);
        }


    }
}
