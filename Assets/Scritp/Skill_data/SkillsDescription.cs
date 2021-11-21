using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2{

    [CreateAssetMenu(fileName = "GameData/CharacterClass", menuName = "SkillsDescription")]
    [System.Serializable]
    public class SkillsDescription : ScriptableObject
    {
        // [SerializeField] CharacterTypeEnum characterType;

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

