using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{


    public class PlayerStateMachineFX 
    {
        public StateFX[] statesViz; // All State we declare 
        public StateType CurrentStateViz; // CurrentState visual we are 



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

        
        /// Every frame:  Check current Animation to end the animation , 
        // If recevie request form Server can active  LogicUpdate() of this State
        public void Update() {
            // Check ALL State that have actual Action correspond ( See in Game Data Soucre Objet )
            if(CurrentStateViz != StateType.Idle && CurrentStateViz != StateType.Move  ){
                Debug.Log(CurrentStateViz + "Visual");
                SkillsDescription skillsDescription =  GetState(CurrentStateViz).SkillDescription(CurrentStateViz); // Get All Skills Data of actual Player Charater we current play.
                bool keepGoing = GetState(CurrentStateViz).Anticipated || GetState(CurrentStateViz).LogicUpdate(); // (Trick of || (or) )only call Update() on actions that are past anticipation , 
                if (skillsDescription.expirable){
                    bool timeExpired =  GetState(CurrentStateViz).TimeRunning >= skillsDescription.DurationSeconds ;
                    // Check if this State Can End Naturally (== time Expired )
                    if ( timeExpired ){
                        GetState(CurrentStateViz)?.End();
                    }
                }
                if (!keepGoing){
                    GetState(CurrentStateViz)?.End();
                }
                
            }
        }

        public void OnAnimEvent(string id)
        {
            GetState(CurrentStateViz).OnAnimEvent(id);
        }

        // Switch to Another State , (we force to Change State , so that mean this State may be not End naturally , be interruped by some logic  ) 
        public void ChangeState( StateType data){
            if (CurrentStateViz != data){
                GetState(CurrentStateViz)?.Exit();
                CurrentStateViz = data;
            }
            GetState(CurrentStateViz)?.Enter();
        }

        // Movement in Client 
        public void OnMoveInput(Vector2 position)
        {
            GetState(CurrentStateViz).SetMovementTarget(position);
        }


    }
}
