using MLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI.Spawning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Assertions;


namespace LF2.Client
{

    public class ClientInputSender : NetworkBehaviour {
        
        #region Movement (Keyborad not importance)
            
        public Vector2 RawMovementInput { get ; private set;} // For keyborad

        // JUMP
        public bool JumpInput{get;private set;}
        [SerializeField] private float inputHoldTime = 0.2f;
        private float jumpInputStartTime;
        // JUMP

        //RUN
        private float lastHoldRightTime;
        private float lastHoldLeftTime;
        public bool canRun {get ; private set;}
        private int countTime;
        //RUN

        Vector2 direction;
        public bool AttackInput{get;private set;}
        public bool DefenseInput{get;private set;}

        
        #endregion

        //COMBO  
        // public KeyPress currentKeyPress{get;private set;}
        // public List<KeyPress> currentCombo = new List<KeyPress>();
        // public List<ComboAttack> avilableSkills;
    

        // public event Action<TypeSkills> ComboTrigger;

        ////// ********* NEW ****** ///
        private NetworkCharacterState m_NetworkCharacter;



        private struct ActionRequest
        {
            // public SkillTriggerStyle TriggerStyle;
            public StateType RequestedAction;
            public ulong TargetId;
        }
 
        private readonly ActionRequest[] m_ActionRequests = new ActionRequest[1];
        public event Action<Vector2> ClientMoveEvent;


        /// <summary>
        /// Convenience getter that returns our CharacterData
        /// </summary>
        CharacterClass CharacterData => GameDataSource.Instance.CharacterDataByType[m_NetworkCharacter.CharacterType];

        
        /// <summary>
        /// This event fires at the time when an action request is sent to the server.
        /// </summary>
        public Action<StateRequestData> ActionInputEvent;
        private int m_ActionRequestCount;


        public float directionMarngitude = 4f;
        public float JumpHieght = 10f;

        // COMBO

        public override void NetworkStart(){
            if (!IsClient || !IsOwner)
                {
                    enabled = false;
                    // dont need to do anything else if not the owner
                    return;
                }

            // var classJumpCombo = GameObject.FindGameObjectWithTag("JumpUI").GetComponent<JumpButton>();            

            // var classAttackCombo = GameObject.FindGameObjectWithTag("AttackUI").GetComponent<AttackButton>();            
            
            // var joystickScreen  = GameObject.FindGameObjectWithTag("Joystick").GetComponent<JoystickScreen>();            
            
            // find the hero action UI bar
            GameObject actionUIobj = GameObject.FindGameObjectWithTag("HeroActionBar");
            actionUIobj.GetComponent<Visual.HeroActionBar>().RegisterInputSender(this);
        }

        private void Awake(){

            m_NetworkCharacter = GetComponent<NetworkCharacterState>();
            

            // joystickScreen.SendControlValue += OnMoveInputUI;
            // classJumpCombo.classJumpComboEvent += PerformCombo;
            // classAttackCombo.classAttackComboEvent += PerformCombo;

            // NGU , only need is run or not , dont need direction
            // var runLeftButton = GameObject.FindObjectOfType<RunLeftButton>().GetComponent<RunLeftButton>();            
            // var runRightButton = GameObject.FindObjectOfType<RunRightButton>().GetComponent<RunRightButton>();           

            // runLeftButton.runLeftEvent += GoRun;
            // runRightButton.runRightEvent += GoRun;

        }

        private void SendInput(StateRequestData action)
        {
            ActionInputEvent?.Invoke(action);
            m_NetworkCharacter.RecvDoActionServerRPC(action);
        }

        // private void GoRun()
        // {
        //     canRun = true;
        // }

        // private void PerformCombo(TypeSkills typeCombo)
        // {
        //     ComboTrigger?.Invoke(typeCombo);
        // }





        public void OnMoveInput(InputAction.CallbackContext context){
            

            RawMovementInput = context.ReadValue<Vector2>();
            // Debug.Log(RawMovementInput);
            if (context.started){
            
                m_NetworkCharacter.SendCharacterInputServerRpc(RawMovementInput);
                //Send to client 
                ClientMoveEvent?.Invoke(RawMovementInput);
            }
            if (context.performed){
                //Send to server 
                m_NetworkCharacter.SendCharacterInputServerRpc(RawMovementInput);

                //Send to client 
                ClientMoveEvent?.Invoke(RawMovementInput);
            }
            if (context.canceled){
                //Send to server 
                m_NetworkCharacter.SendCharacterInputServerRpc(RawMovementInput);

                //Send to client 
                ClientMoveEvent?.Invoke(RawMovementInput);
            }
        
        }
        public void OnMoveInputUI(Vector2 inputUI){
                              
            //Send to server 
            m_NetworkCharacter.SendCharacterInputServerRpc(inputUI);

            //Send to client 
            ClientMoveEvent?.Invoke(inputUI);
            // a changer 
            direction.Set(inputUI.x * directionMarngitude,JumpHieght) ;
                       
        }

        #region KEyboard (Not importance)
             
        public void OnJumpInput(InputAction.CallbackContext context){
            if (context.started){
                JumpInput = true;
                jumpInputStartTime = Time.time;
                // IF some character can jump different with other , 
                // Need to specifie in CharacterData.Skill or .Jump (specific)
                RequestAction(StateType.Jump);
            }

        }
        
        public void OnAttackInput(InputAction.CallbackContext context){
            if (context.started){
                Debug.Log("OnAttackInput");
                AttackInput = true;
                // Same with Jump
                RequestAction(StateType.Attack);
            }
        
        }

        public void OnDefenseInput(InputAction.CallbackContext context){
            if (context.started){
                // DefenseInput = true;
                RequestAction(StateType.Defense);
            }
       
        }


        public void UseJumpInput() => JumpInput = false;



        public void ResetRun(){
            canRun = false;
        }
        #endregion


        /// <summary>
        /// Request an State be performed. This will occur on the next FixedUpdate.
        /// </summary>
        /// <param name="action">the action you'd like to perform. </param>

            // In the furture may be we can developp this feature
        /// <param name="triggerStyle">What input style triggered this action.</param>
        public void RequestAction(StateType action, ulong targetId = 0)
        {


            if (m_ActionRequestCount < m_ActionRequests.Length)
            {
                m_ActionRequests[m_ActionRequestCount].RequestedAction = action;
                // m_ActionRequests[m_ActionRequestCount].TriggerStyle = triggerStyle;
                m_ActionRequests[m_ActionRequestCount].TargetId = targetId;
                m_ActionRequestCount++;
            }
    

        }

        /// <summary>
        /// Perform a skill in response to some input trigger. This is the common method to which ALL (Keyborad , UI ....) input-driven skill plays funnel.
        /// </summary>
        /// <param name="actionType">The action you want to play. Note that "Skill1" may be overriden contextually depending on the target.</param>
        /// <param name="triggerStyle">What sort of input triggered this skill?</param>
        /// <param name="targetId">(optional) Pass in a specific networkID to target for this action</param>
        private void PerformSkill(StateType actionType, ulong targetId = 0)
        {
            // In that time we can extend data more 
            // But now only StateType are send 
            var data = new StateRequestData();
            data.StateTypeEnum = actionType;

            SendInput(data);

        }

        private void FixedUpdate() {
   
            // It really make any sense to use for 
            // So this code may be can change (I dont know)
            for (int i = 0; i < m_ActionRequestCount; ++i)
            {
                PerformSkill(m_ActionRequests[i].RequestedAction,  m_ActionRequests[0].TargetId); 
            }
            m_ActionRequestCount = 0;
        }

        
       
    }
}
