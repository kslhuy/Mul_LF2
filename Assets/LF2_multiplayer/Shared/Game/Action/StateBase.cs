using UnityEngine;

namespace LF2
{
    /// <summary>
    /// Abstract base class containing some common members shared by Action (server) and ActionFX (client visual)
    /// </summary>
    public abstract class StateBase
    {
        protected CharacterTypeEnum CharacterType;



        /// <summary>
        /// Time when this Action was started (from Time.time) in seconds. Set by the ActionPlayer or ActionVisualization.
        /// </summary>
        public float TimeStarted { get; set; }

        /// <summary>
        /// How long the Action has been running (since its Start was called)--in seconds, measured via Time.time.
        /// </summary>
        public float TimeRunning { get { return (Time.time - TimeStarted); } }



        // / <summary>
        // / Data Description for this action.
        // / </summary>
        private CharacterSkillsDescription m_CharacterSkillsDescription
        {
            get
            {
                CharacterSkillsDescription result;
                var found = GameDataSource.Instance.CharacterSkillDataByType.TryGetValue(CharacterType , out result);
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


        public bool AnimationActionTrigger;
        public StateBase(CharacterTypeEnum characterType)
        {
            CharacterType = characterType;
        }

    }

}
