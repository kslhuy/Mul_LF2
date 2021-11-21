using UnityEngine;
using System.Collections.Generic;

using UnityEngine.Serialization;

namespace LF2
{
    /// <summary>
    /// Data representation of a Character, containing such things as its starting HP and Mana, and what attacks it can do.
    /// </summary>
    [CreateAssetMenu(menuName = "GameData/CharacterClass", order = 1)]
    public class CharacterClass : ScriptableObject
    {
        [Tooltip("which character this data represents")]
        public CharacterTypeEnum CharacterType;

        public List<SkillsDescription> SkillsDescription; //The kind of the move

        [Tooltip("Starting HP of this character class")]
        public IntVariable BaseHP;

        [Tooltip("Starting Mana of this character class")]
        public int BaseMana;

        [Tooltip("Base movement speed of this character class (in meters/sec)")]
        public float Speed;

        [Tooltip("Set to true if this represents an NPC, as opposed to a player.")]
        public bool IsNpc;

        [Tooltip("For NPCs, this will be used as the aggro radius at which enemies wake up and attack the player")]
        public float DetectRange;

        [Tooltip("For players, this is the displayed \"class name\". (Not used for monsters)")]
        public string DisplayedName;

        [Tooltip("For players, this is the class banner (when active). (Not used for monsters)")]
        public Sprite ClassBannerLit;

        [Tooltip("For players, this is the class banner (when inactive). (Not used for monsters)")]
        public Sprite ClassBannerUnlit;

        
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
