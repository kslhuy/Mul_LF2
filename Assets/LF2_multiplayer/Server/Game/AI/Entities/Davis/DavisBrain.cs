using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    class DavisBrain : AIBrain 
    {
        
        public DavisBrain(DavisCharacter me) : base(me)
        {

            m_Logics = new Dictionary<AIStateType, AIState>
            {
                [AIStateType.IDLE] = new davisIdle(this),
     
            };
            
            m_Logics.Add(AIStateType.ATTACK, new daivsAttack(this));                
            m_Logics.Add(AIStateType.MOVE, new davisMove(this));
            m_CurrentState = AIStateType.IDLE;
            
        }

        public void Update()
        {

            m_Logics[m_CurrentState].Update();
            Debug.Log(m_CurrentState);
        }

    }
}

