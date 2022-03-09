using UnityEngine;

namespace LF2
{
    /// <summary>
    /// Abstract base class containing some common members shared by Action (server) and ActionFX (client visual)
    /// </summary>
    public abstract class stateMachineBase
    {
        protected CharacterTypeEnum CharacterType;
        public CharacterSkillsDescription m_CharacterSkillsDescription
        {

            get
            {
                CharacterSkillsDescription result;
                var found = GameDataSource.Instance.CharacterSkillDataByType.TryGetValue(CharacterType, out result);
                // Debug.Log(result);
                Debug.AssertFormat(found, "Tried to find StateType but it was missing from GameDataSource!");
                return result;
            }
        }


        public virtual SkillsDescription SkillDescription(StateType stateType){
            SkillsDescription value ;
            var found = m_CharacterSkillsDescription.SkillDataByType.TryGetValue(stateType , out value);
            Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", stateType);
            return value;
            //           Debug.Log(result);
            // Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", Data.StateTypeEnum);
        }
        
        public stateMachineBase(CharacterTypeEnum characterType)
        {
            CharacterType = characterType;
        }
    }

}
