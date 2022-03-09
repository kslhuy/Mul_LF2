using UnityEngine.Assertions;
using UnityEngine;
using System.Collections.Generic;
using LF2.Visual;

namespace LF2.Client
{
    /// <summary>
    /// Responsible for storing of all of the pieces of a character, and swapping the pieces out en masse when asked to.
    /// </summary>
    public class CharacterSwap : MonoBehaviour
    {
        [System.Serializable]
        public class CharacterModelSet
        {
            public Visual.AnimatorTriggeredSpecialFX specialFx; // should be a component on the same GameObject as the Animator!
            public AnimatorOverrideController animatorOverrides; // references a separate stand-alone object in the project

        }

        [SerializeField]
        CharacterModelSet m_CharacterModel;

        /// <summary>
        /// Reference to our shared-characters' animator.
        /// Can be null, but if so, animator overrides are not supported!
        /// </summary>
        [SerializeField]
        private Animator m_Animator;

        /// <summary>
        /// Reference to the original controller in our Animator.
        /// We switch back to this whenever we don't have an Override.
        /// </summary>
        private RuntimeAnimatorController m_OriginalController;

        ClientCharacterVisualization m_ClientCharacterVisualization;

        private void Awake()
        {
            m_ClientCharacterVisualization = GetComponentInParent<ClientCharacterVisualization>();
            m_Animator = m_ClientCharacterVisualization.OurAnimator;
            m_OriginalController = m_Animator.runtimeAnimatorController;
        }


        /// <summary>
        /// Swap the visuals of the character to the index passed in.
        /// </summary>
        /// <param name="modelIndex">Zero-based array index of the model</param>
        /// <param name="specialMaterialMode">Special Material to apply to all body parts</param>
        public void SwapToModel()
        {

            if (m_CharacterModel.specialFx)
            {
                m_CharacterModel.specialFx.enabled = true;
            }

            if (m_Animator)
            {
                // plug in the correct animator override... or plug the original non - overridden version back in!
                if (m_CharacterModel.animatorOverrides)
                {
                    m_Animator.runtimeAnimatorController = m_CharacterModel.animatorOverrides;
                }
                else
                {
                    m_Animator.runtimeAnimatorController = m_OriginalController;
                }
            }

        }





#if UNITY_EDITOR
        private void OnValidate()
        {
            // if an Animator is on the same GameObject as us, assume that's the one we'll be using!
            if (!m_Animator)
                m_Animator = GetComponent<Animator>();
        }
#endif
    }
}
