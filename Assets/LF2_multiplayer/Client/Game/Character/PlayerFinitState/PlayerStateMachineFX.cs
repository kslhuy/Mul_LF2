using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{

    /// <summary>
    /// This is a companion class to ClientCharacterVisualization that is specifically responsible for visualizing State. 

    //     
    /// The flow for Visual is:
    /// Initially: Aniticipate() only play action 
    /// if recevie signal from Server , call PlayState() to active  LogicUpdate() + PhysicUpdate() 
  


    /// </summary>
    public class PlayerStateMachineFX : stateMachineBase
    {

        public StateFX[] statesViz = new StateFX[Enum.GetNames(typeof(StateType)).Length]; // All State we declare 
        public StateFX CurrentStateViz; // CurrentState visual we are 

        private StateFX m_lastStateViz; 

        private SkillsDescription skillsDescription;

        public Vector3 moveDir;
        
        public ClientCharacterVisualization m_ClientVisual { get; }

        public ClientCoreMovement CoreMovement  ;

        public PlayerStateMachineFX(ClientCharacterVisualization parent , CharacterTypeEnum characterType ) : base(characterType)
        {
            
            m_ClientVisual = parent;
            CoreMovement = parent.coreMovement; 
            
            // Intiliazie State
            RegisterState(new PlayerIdleStateFX(this ));
            RegisterState(new PlayerMoveStateFX(this ));
            RegisterState(new PlayerRunStateFX(this));
            RegisterState(new SlidingStateFX(this));
            RegisterState(new PlayerRollingStateFX(this));

            RegisterState(new PlayerJumpStateFX(this ));
            RegisterState(new PlayerDoubleJumpStateFX(this ));
            

            RegisterState(new PlayerLandStateFX(this ));

            RegisterState(new PlayerAttackStateFX(this ));
            RegisterState(new PlayerAttackJump1FX(this ));
            RegisterState(new PlayerDefenseStateFX(this));

            RegisterState(new PlayerHurtStateFX(this));
            RegisterState(new PlayerFallStateFX(this));




            RegisterState(new PlayerDDAStateFX(this));
            RegisterState(new PlayerDDJStateFX(this));
            RegisterState(new PlayerDUAStateFX(this));
            RegisterState(new PlayerDUJStateFX(this));


            CurrentStateViz = GetState(StateType.Idle) ;
        }

        // RegisterState , instantiated in the first time 
        public void RegisterState(StateFX state){
            int index = (int)state.GetId();
            statesViz[index] = state;
        }

        // Aticipate State in CLient , 
        // Do change state but most importance its run Animation predict   
        public void AnticipateState(ref StateRequestData data)
        {
            CurrentStateViz.AnticipateState(ref data);
        }

        // Play correct State that sent by Server (broad-cast to all client)
        public void PerformActionFX(ref StateRequestData data)
        {
            GetState(data.StateTypeEnum).Data = data;
            ChangeState(data.StateTypeEnum);
        }

        // Movement in Client 
        public void OnMoveInput(Vector2 position)
        {
            CurrentStateViz.SetMovementTarget(position);
            moveDir.Set(position.x , 0, position.y);   

        }
        
      
        // Do convert enum StateType == > State corresponse 
        public StateFX GetState (StateType stateType){
            int index = (int)stateType;
            return statesViz[index];
        }

        
        /// Every frame:  Check current Animation to end the animation , 
        // If recevie request form Server can active  LogicUpdate() of this State
        public void Update() {
            // Check ALL State that have actual Action correspond ( See in Game Data Soucre Objet )

            Debug.Log(CurrentStateViz);
            if (CurrentStateViz.GetId() == StateType.Idle) return;

            if (CurrentStateViz.GetId() == StateType.Move){
                m_lastStateViz = CurrentStateViz;
                CurrentStateViz.LogicUpdate();
                return;
            }

            if ( m_lastStateViz != CurrentStateViz){
                m_lastStateViz = CurrentStateViz;
                if (CurrentStateViz.GetId() == StateType.Idle || CurrentStateViz.GetId() == StateType.Move ) return;
                skillsDescription =  SkillDescription(CurrentStateViz.GetId()); // Get All Skills Data of actual Player Charater we current play.
            } 

            if (skillsDescription!= null){
                if (skillsDescription.expirable)
                {
                    // Debug.Log($"Sub_TimeAnimation = {Time.time -  CurrentStateViz.TimeStarted_Animation} "); 
                    bool timeExpired = Time.time -  CurrentStateViz.TimeStarted_Animation >= skillsDescription.DurationSeconds;
                    // Check if this State Can End Naturally (== time Expired )
                    if ( timeExpired ){
                        CurrentStateViz?.End();
                        return;
                    }
                }
            }
            CurrentStateViz.LogicUpdate();

        }

        public void OnAnimEvent(string id)
        {
            CurrentStateViz.OnAnimEvent(id);
        }

        // Switch to Another State , (we force to Change State , so that mean this State may be not End naturally , be interruped by some logic  ) 
        ///  Only exucute when server call !!!!
        public void ChangeState( StateType state  ){
            CurrentStateViz?.Exit();
            CurrentStateViz = GetState(state);
            CurrentStateViz?.Enter();
        }

        /// <summary>
        /// Tells all active Actions that a particular gameplay event happened, such as being hit,
        /// getting healed, dying, etc. Actions can change their behavior as a result.
        /// </summary>
        /// <param name="activityThatOccurred">The type of event that has occurred</param>
        public void OnGameplayActivityVisual(ref StateRequestData data)
        {
            if (CurrentStateViz.GetId() != data.StateTypeEnum){
                CurrentStateViz = GetState(data.StateTypeEnum) ;
            }
            CurrentStateViz.OnGameplayActivity(data);
        }


        public void idle()
        {
            GetState(StateType.Idle).PlayAnim(StateType.Idle);
        }

        public void OnTriggerEnter(Collider collider) {
            CurrentStateViz?.AddCollider(collider);
        }
        public void OnTriggerExit(Collider collider) {
            CurrentStateViz?.RemoveCollider(collider);
        }
    }
    
    
    
}
