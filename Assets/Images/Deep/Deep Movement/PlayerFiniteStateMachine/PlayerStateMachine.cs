using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    public class PlayerStateMachine 
    {
        public PlayerState CurrentState{ get ; private set;}
        public void Initialize(PlayerState startingState){
            CurrentState = startingState;
            CurrentState.Enter();
        }
        public void ChangeState(PlayerState newState){
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void AdvanceQueue(ref ActionRequestData actionRequestData){
            // if (CurrentState == PlayerIdleState)
        }

        // public bool GetActiveStateInfo(out ActionRequestData data)
        // {
        //     if (m_Queue.Count > 0)
        //     {
        //         data = m_Queue[0].Data;
        //         return true;
        //     }
        //     else
        //     {
        //         data = new ActionRequestData();
        //         return false;
        //     }
        // }


    }
}
