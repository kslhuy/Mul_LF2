using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Visual{
    public class PlayerRollingState : StateFX
    {
        float rollingSpeed;
        float distanceRolling;
        private float distanceRollingTravelled;

        public PlayerRollingState(CharacterTypeEnum characterType, PlayerStateFX m_PlayerFX) : base(characterType, m_PlayerFX)
        {
        }

        public override void AnticipateState(ref StateRequestData requestData)
        {
            base.AnticipateState(ref requestData);
        }


        public override void End()
        {
            base.End();
        }

        public override void Enter()
        {
            base.Enter();
        }



        public override void Exit()
        {
            base.Exit();
        }



        public override StateType GetId()
    {
        return StateType.Rolling;
    }

        public override bool LogicUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnAnimEvent(string id)
        {
            base.OnAnimEvent(id);
        }

        public override void PlayAnim(StateType currentState, int nbAniamtion = 0)
        {
            base.PlayAnim(currentState, nbAniamtion);
        }

        public override void SetMovementTarget(Vector2 position)
        {
            base.SetMovementTarget(position);
        }

        public override SkillsDescription SkillDescription(StateType stateType)
        {
            return base.SkillDescription(stateType);
        }


    }
}
