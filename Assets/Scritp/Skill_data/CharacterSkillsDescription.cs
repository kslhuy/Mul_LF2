using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2{

    [CreateAssetMenu(fileName = "GameData/CharacterClass", menuName = "CharacterSkillsDescription")]
    [System.Serializable]
    public class CharacterSkillsDescription : ScriptableObject
    {
        public CharacterTypeEnum CharacterType;

        public List<SkillsDescription> SkillsDescription; //The kind of the move


        // public StateType StateType; //The kind of the move
        
        // public int damageAmount;

        // public int ManaCost;

        // public float Range;

        // public Vector3 velocity;
        // // public AnimationCurve animationCurve;

        // public float DurationSeconds;

        // // [SerializeField] int ComboPriorty = 0; //the more complicated the move the higher the Priorty
        

                
        private Dictionary<StateType, SkillsDescription> m_SkillDataMap;

        public  Dictionary<StateType, SkillsDescription> SkillDataByType{
            get
                {
                    if( m_SkillDataMap == null )
                    {
                        m_SkillDataMap = new Dictionary<StateType, SkillsDescription>();
                        // Hoi bi rac roi cach viet
                        // co 1 list SkillsDescription o tren , lay tung cai 1 .
                        foreach (SkillsDescription data in SkillsDescription)
                        {
                            if (m_SkillDataMap.ContainsKey(data.StateType))
                            {
                                throw new System.Exception($"Duplicate action definition detected: {data.StateType}");
                            }
                            m_SkillDataMap[data.StateType] = data;
                        }
                    }
                    return m_SkillDataMap;
                }
        }
    }
}

