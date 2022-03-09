using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LF2.Server
{
    /// <summary>
    /// Handles enemy AI. Contains AIStateLogics that handle some of the details,
    /// and has various utility functions that are called by those AIStateLogics
    /// </summary>

    /// == Entity
    public class AIBrain
    {
        static readonly AIStateType[] k_AIStates = (AIStateType[])Enum.GetValues(typeof(AIStateType));

        public ServerCharacter m_ServerCharacter;

        public ServerCharacterMovement Movement;
         
        protected AIStateType m_CurrentState;
        protected Dictionary<AIStateType, AIState> m_Logics;
        private List<ServerCharacter> m_HatedEnemies;


        
        public  AIBrain(ServerCharacter me){
            m_ServerCharacter = me;
            Movement = m_ServerCharacter.Movement;
 
            m_HatedEnemies = new List<ServerCharacter>();


        }

        public void ChangeState(AIStateType newState){
            m_Logics[m_CurrentState].OnExit();
            m_CurrentState = newState;
            m_Logics[newState].OnEnter();
        }
                 

                /// <summary>
        /// Returns true if it be appropriate for us to murder this character, starting right now!
        /// </summary>
        public bool IsAppropriateFoe(ServerCharacter potentialFoe)
        {
            if (potentialFoe == null ||
                potentialFoe.IsNpc ||
                potentialFoe.NetState.LifeState != LifeState.Alive ||
                potentialFoe.NetState.IsStealthy.Value)
            {
                return false;
            }

            // Also, we could use NavMesh.Raycast() to see if we have line of sight to foe?
            return true;
        }

        /// <summary>
        /// Notify the AIBrain that we should consider this character an enemy.
        /// </summary>
        /// <param name="character"></param>
        public void Hate(ServerCharacter character)
        {
            if (!m_HatedEnemies.Contains(character))
            {
                m_HatedEnemies.Add(character);
            }
        }

        /// <summary>
        /// Return the raw list of hated enemies -- treat as read-only!
        /// </summary>
        public List<ServerCharacter> GetHatedEnemies()
        {
            // first we clean the list -- remove any enemies that have disappeared (became null), are dead, etc.
            for (int i = m_HatedEnemies.Count - 1; i >= 0; i--)
            {
                if (!IsAppropriateFoe(m_HatedEnemies[i]))
                {
                    m_HatedEnemies.RemoveAt(i);
                }
            }
            return m_HatedEnemies;
        }

        public ServerCharacter searchClosestTarget( Vector3 myPosition)
        {
            // Choise the closet target


            float closestDistanceSqr = int.MaxValue;
            ServerCharacter closestFoe = null;
            foreach (var foe in GetHatedEnemies())
            {
                float distanceSqr = (myPosition - foe.physicsWrapper.Transform.position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestFoe = foe;
                }
            }
            return closestFoe;
        }

        /// <summary>
        /// Retrieve info about who we are. Treat as read-only!
        /// </summary>
        /// <returns></returns>
        public ServerCharacter GetMyServerCharacter()
        {
            return m_ServerCharacter;
        }


    }
}
