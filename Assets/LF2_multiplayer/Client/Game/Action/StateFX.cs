using UnityEngine;
using System.Collections.Generic;
using System;

namespace LF2.Visual{

    /// <summary>
    /// Abstract base class for playing back the visual feedback of Current State.
    /// </summary>
    public abstract class StateFX {

        protected PlayerStateMachineFX MPlayerMachineFX;

        public StateRequestData Data;

        public bool IsMove { get; private set; }

 
        // Constructor 
        protected StateFX(PlayerStateMachineFX mPlayerMachineFX)
        {
            this.MPlayerMachineFX = mPlayerMachineFX;
        }

        public float TimeStarted_Animation { get; private set; }

        public bool Anticipated { get; protected set; }

        public abstract StateType GetId();

        // Alaways check if player are already play animation first
        public virtual void Enter(){
            Anticipated = false; //once you start for real you are no longer an anticipated action.
            TimeStarted_Animation = Time.time;
            // NOTE TimeStarted_Animation in Hurt State can be refesh many time to extend duree the cycle of this State 

        }
        public virtual void LogicUpdate(){

        }


        // Interrupt by somthing
        public virtual void Exit(){
            // Anticipated = false;
        }

        public virtual void AddCollider(Collider collider){}
        public virtual void RemoveCollider(Collider collider){}


        /// <summary>
        /// Called when the visualization receives an animation event.
        /// </summary>
        public virtual void OnAnimEvent(string id) { }

        // Play Animation (shoulde be add base.PlayAnim() in specific (class) that derived from State ) 
        // See in class AttackStateFX 
        // Call everyttme when we want change State in  Visual !!!! 

        public virtual void  PlayAnim(StateType state , int nbAniamtion = 0 ){

            Anticipated = true;
            TimeStarted_Animation = Time.time;  
            MPlayerMachineFX.ChangeState(state);
        }

        public virtual void SetMovementTarget(Vector2 position)
        {
            IsMove  = position.x != 0 || position.y != 0;
        }

        public virtual void AnticipateState(ref StateRequestData requestData)
        {
        }

        /// <summary>
        /// Utility function that instantiates all the graphics in the Spawns list.
        /// If parentToOrigin is true, the new graphics are parented to the origin Transform.
        /// If false, they are positioned/oriented the same way but are not parented.
        /// </summary>
        protected List<SpecialFXGraphic> InstantiateSpecialFXGraphics(Transform origin, bool parentToOrigin , StateType stateType)
        {
            var returnList = new List<SpecialFXGraphic>();
            foreach (var prefab in MPlayerMachineFX.SkillDescription(stateType).Spawns)
            {
                if (!prefab) { continue; } // skip blank entries in our prefab list
                returnList.Add(InstantiateSpecialFXGraphic(prefab, origin, parentToOrigin,stateType));
            }
            return returnList;
        }

        /// <summary>
        /// Utility function that instantiates one of the graphics from the Spawns list.
        /// If parentToOrigin is true, the new graphics are parented to the origin Transform.
        /// If false, they are positioned/oriented the same way but are not parented.
        /// </summary>
        protected SpecialFXGraphic InstantiateSpecialFXGraphic(GameObject prefab, Transform origin, bool parentToOrigin,StateType stateType)
        {
            if (prefab.GetComponent<SpecialFXGraphic>() == null)
            {
                throw new System.Exception($"One of the Spawns on action {MPlayerMachineFX.SkillDescription(stateType).ComboLogic} does not have a SpecialFXGraphic component and can't be instantiated!");
            }
            var graphicsGO = GameObject.Instantiate(prefab, origin.transform.position, origin.transform.rotation, (parentToOrigin ? origin.transform : null));
            return graphicsGO.GetComponent<SpecialFXGraphic>();
        }
 

        public virtual void OnGameplayActivity(StateRequestData data) { }

        public virtual void End()
        {
            MPlayerMachineFX.idle();
        }
    }
}
   