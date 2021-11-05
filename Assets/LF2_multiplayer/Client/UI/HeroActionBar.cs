using MLAPI;
using MLAPI.Spawning;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
// using SkillTriggerStyle = LF2.Client.ClientInputSender.SkillTriggerStyle;
using UnityEngine.InputSystem.OnScreen;

namespace LF2.Visual
{
    /// <summary>
    /// Provides logic for a Hero Action Bar with attack, skill buttons and a button to open emotes panel
    /// This bar tracks button clicks on hero action buttons for later use by ClientInputSender
    /// </summary>

    public class HeroActionBar : MonoBehaviour
    {
        [SerializeField]
        AttackButton m_AttackButton;

        [SerializeField]
        JumpButton m_JumpButton;

        [SerializeField]
        DefenseButton m_DefenseButton;

        [SerializeField]
        JoystickScreen m_JoystickScreen;

        [SerializeField]
        DownSlotButton m_DownSlotButton;

        [SerializeField]
        UpSlotButton m_UpSlotButton;

        // [SerializeField]
        // [Tooltip("The button that opens/closes the Emote bar")]
        // UIHUDButton m_EmoteBarButton;

        // [SerializeField]
        // [Tooltip("The Emote bar that will be enabled or disabled when clicking the Emote bar button")]
        // GameObject m_EmotePanel;

        /// <summary>
        /// Our input-sender. Initialized in RegisterInputSender()
        /// </summary>
        Client.ClientInputSender m_InputSender;

        /// <summary>
        /// Cached reference to local player's net state.
        /// We find the Sprites to use by checking the Skill1, Skill2, and Skill3 members of our chosen CharacterClass
        /// </summary>
        NetworkCharacterState m_NetState;

        /// <summary>
        /// If we have another player selected, this is a reference to their stats; if anything else is selected, this is null
        /// </summary>
        NetworkCharacterState m_SelectedPlayerNetState;

        /// <summary>
        /// If m_SelectedPlayerNetState is non-null, this indicates whether we think they're alive. (Updated every frame)
        /// </summary>
        bool m_WasSelectedPlayerAliveDuringLastUpdate;

        /// <summary>
        /// Identifiers for the buttons on the action bar.
        /// </summary>





        /// <summary>
        /// Called during startup by the ClientInputSender. In response, we cache the provided
        /// inputSender and self-initialize.
        /// </summary>
        /// <param name="inputSender"></param>
        public void RegisterInputSender(Client.ClientInputSender inputSender)
        {
            if (m_InputSender != null)
            {
                Debug.LogWarning($"Multiple ClientInputSenders in scene? Discarding sender belonging to {m_InputSender.gameObject.name} and adding it for {inputSender.gameObject.name} ");
            }

            m_InputSender = inputSender;
            m_NetState = m_InputSender.GetComponent<NetworkCharacterState>();
            // m_NetState.TargetId.OnValueChanged += OnSelectionChanged;
            // UpdateAllActionButtons();
        }

        // void Awake()
        // {
        //     m_ButtonInfo = new Dictionary<ActionButtonType, ActionButtonInfo>()
        //     {
        //         [ActionButtonType.BasicAction] = new ActionButtonInfo(ActionButtonType.BasicAction, m_AttackButton, this),
        //         [ActionButtonType.Special1] = new ActionButtonInfo(ActionButtonType.Special1, m_JumpButton, this),
        //         [ActionButtonType.Special2] = new ActionButtonInfo(ActionButtonType.Special2, m_DefenseButton, this),
        //         [ActionButtonType.EmoteBar] = new ActionButtonInfo(ActionButtonType.EmoteBar, m_EmoteBarButton, this),
        //     };

        // }

        void OnEnable()
        {
            // foreach (ActionButtonInfo buttonInfo in m_ButtonInfo.Values)
            // {
            //     buttonInfo.RegisterEventHandlers();
            // }

            m_JoystickScreen.SendControlValue += JoystickDrag;
            m_AttackButton.AttackAction += OnAtack;
            m_DefenseButton.DefenseAction += OnDefense;
            m_JumpButton.JumpAction += OnJump;


        }

        void OnDisable()
        {
            // foreach (ActionButtonInfo buttonInfo in m_ButtonInfo.Values)
            // {
            //     buttonInfo.UnregisterEventHandlers();
            // }
        }

        void OnDestroy()
        {
            // if (m_NetState)
            // {
            //     m_NetState.TargetId.OnValueChanged -= OnSelectionChanged;
            // }
        }

        void Update()
        {
            // If we have another player selected, see if their aliveness state has changed,
            // and if so, update the interactiveness of the basic-action button

            if (!m_SelectedPlayerNetState) { return; }

            bool isAliveNow = m_SelectedPlayerNetState.NetworkLifeState.LifeState.Value == LifeState.Alive;
            if (isAliveNow != m_WasSelectedPlayerAliveDuringLastUpdate)
            {
                // this will update the icons so that the basic-action button's interactiveness is correct
                // UpdateAllActionButtons();
            }

            m_WasSelectedPlayerAliveDuringLastUpdate = isAliveNow;
        }

        // void OnSelectionChanged(ulong oldSelectionNetworkId, ulong newSelectionNetworkId)
        // {
        //     UpdateAllActionButtons();
        // }

        // void OnButtonClickedDown(ActionButtonType buttonType)
        // {
        //     // if (buttonType == ActionButtonType.EmoteBar)
        //     // {
        //     //     return; // this is the "emote" button; we won't do anything until they let go of the button
        //     // }

        //     if (m_InputSender == null)
        //     {
        //         //nothing to do past this point if we don't have an InputSender.
        //         return;
        //     }

        //     // send input to begin the action associated with this button
        //     m_InputSender.RequestAction(m_ButtonInfo[buttonType].CurActionType);
        // }

        void OnAtack()
        {
            // send input to begin the action associated with this button
            m_InputSender.RequestAction(ActionType.AttackGeneral);
        }

        void OnJump()
        {
            // send input to begin the action associated with this button
            m_InputSender.RequestAction(ActionType.JumpGeneral);
        }

        void OnDefense()
        {
            // send input to begin the action associated with this button
            m_InputSender.RequestAction(ActionType.DefenseGeneral);
        }

        // void OnButtonClickedUp(ActionButtonType buttonType)
        // {
        //     if (buttonType == ActionButtonType.EmoteBar)
        //     {
        //         m_EmotePanel.SetActive(!m_EmotePanel.activeSelf);
        //         return;
        //     }

        //     if (m_InputSender == null)
        //     {
        //         //nothing to do past this point if we don't have an InputSender.
        //         return;
        //     }

        //     // send input to complete the action associated with this button
        //     m_InputSender.RequestAction(m_ButtonInfo[buttonType].CurActionType, SkillTriggerStyle.UIRelease);
        // }

        void JoystickDrag(Vector2 position)
        {

            // send input to begin the action associated with this button
            m_InputSender.OnMoveInputUI(position);
        }

        /// <summary>
        /// Updates all the action buttons and caches info about the currently-selected entity (when appropriate):
        /// stores info in m_SelectedPlayerNetState and m_WasSelectedPlayerAliveDuringLastUpdate
        /// </summary>
        // void UpdateAllActionButtons()
        // {
        //     UpdateActionButton(m_ButtonInfo[ActionButtonType.BasicAction], m_NetState.CharacterData.Skill1);
        //     UpdateActionButton(m_ButtonInfo[ActionButtonType.Special1], m_NetState.CharacterData.Skill2);
        //     UpdateActionButton(m_ButtonInfo[ActionButtonType.Special2], m_NetState.CharacterData.Skill3);

        //     // // special case: when we have a player selected, we change the meaning of the basic action
        //     // if (m_NetState.TargetId.Value != 0
        //     //     && NetworkSpawnManager.SpawnedObjects.TryGetValue(m_NetState.TargetId.Value, out NetworkObject selection)
        //     //     && selection != null
        //     //     && selection.IsPlayerObject
        //     //     && selection.NetworkObjectId != m_NetState.NetworkObjectId)
        //     // {
        //     //     // we have another player selected! In that case we want to reflect that our basic Action is a Revive, not an attack!
        //     //     // But we need to know if the player is alive... if so, the button should be disabled (for better player communication)

        //     //     var charState = selection.GetComponent<NetworkCharacterState>();
        //     //     Assert.IsNotNull(charState); // all PlayerObjects should have a NetworkCharacterState component

        //     //     bool isAlive = charState.NetworkLifeState.LifeState.Value == LifeState.Alive;
        //     //     UpdateActionButton(m_ButtonInfo[ActionButtonType.BasicAction], ActionType.GeneralRevive, !isAlive);

        //     //     // we'll continue to monitor our selected player every frame to see if their life-state changes.
        //     //     m_SelectedPlayerNetState = charState;
        //     //     m_WasSelectedPlayerAliveDuringLastUpdate = isAlive;
        //     // }
        //     // else
        //     // {
        //     //     m_SelectedPlayerNetState = null;
        //     //     m_WasSelectedPlayerAliveDuringLastUpdate = false;
        //     // }
        // }

        // void UpdateActionButton( ActionType actionType, bool isClickable = true)
        // {
        //     // first find the info we need (sprite and description)
        //     Sprite sprite = null;
        //     string description = "";

        //     if (actionType != ActionType.None)
        //     {
        //         var desc = GameDataSource.Instance.ActionDataByType[actionType];
        //         sprite = desc.Icon;
        //         description = desc.Description;
        //     }

        //     // set up UI elements appropriately
        //     if (sprite == null)
        //     {
        //         buttonInfo.Button.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         buttonInfo.Button.gameObject.SetActive(true);
        //         buttonInfo.Button.interactable = isClickable;
        //         buttonInfo.Button.image.sprite = sprite;
        //         // buttonInfo.Tooltip.SetText(description);
        //     }

        //     // store the action type so that we can retrieve it in click events
        //     buttonInfo.CurActionType = actionType;
        // }
    }
}
