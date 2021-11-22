using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2{
    /// <summary>
    /// Data description of a single State of sigle Player (For exemple Attack of David), including the information to visualize it (animations etc), and the information
    /// to play it back on the server.
    /// </summary>
    [CreateAssetMenu(fileName = "GameData/CharacterClass", menuName = "SkillsDescription")]
    [System.Serializable]
    public class SkillsDescription : ScriptableObject
    {
        public StateType StateType; //The kind of the move
        
        public int damageAmount;

        public int ManaCost;

        public float Range;

        public Vector3 velocity;
        // public AnimationCurve animationCurve;

        public float DurationSeconds;

        public bool expirable;

        // [SerializeField] int ComboPriorty = 0; //the more complicated the move the higher the Priorty
        
    }
}

