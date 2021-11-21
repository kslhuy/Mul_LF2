using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2{    
    [CreateAssetMenu(fileName = "SkillsData", menuName = "SkillsData/Type")]
    public class SkillsData : ScriptableObject
    {

        [SerializeField] List<SkillsDescription> SkillsDescription; //The kind of the move
        [SerializeField] CharacterTypeEnum characterType;
        // [SerializeField] TypeSkills typeSkills;

        
        // [Tooltip("Could be damage, could be healing, or other things. This is a base, nominal value that will get modified by game logic when the action takes effect")]
        // private int damageAmount;

        // [Tooltip("How much it costs in Mana to play this Action")]
        // private int ManaCost;

        // [Tooltip("How far the Action performer can be from the Target")]
        // private float Range;

        // [Tooltip("Duration in seconds that this Action takes to play")]
        // private float DurationSeconds;
        public CharacterTypeEnum CharacterType{
            get {return characterType;}
        }



        private Dictionary<TypeSkills, SkillsDescription> m_SkillDataMap;

        public  Dictionary<TypeSkills, SkillsDescription> SkillDataByType{
            get
                {
                    if( m_SkillDataMap == null )
                    {
                        m_SkillDataMap = new Dictionary<TypeSkills, SkillsDescription>();
                        // Hoi bi rac roi cach viet
                        // co 1 list SkillsDescription o tren , lay tung cai 1 .
                        foreach (SkillsDescription data in SkillsDescription)
                        {
                            if (m_SkillDataMap.ContainsKey(data.TypeAction))
                            {
                                throw new System.Exception($"Duplicate action definition detected: {data.TypeAction}");
                            }
                            m_SkillDataMap[data.TypeAction] = data;
                        }
                    }
                    return m_SkillDataMap;
                }
        }


    }
}
