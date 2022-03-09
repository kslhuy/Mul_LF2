using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{

    //In this State :  Player Stand Still  , do nothing , wait to request. 
    //                  Can do some initilize (for Exemple reset number Jump)
    public class PlayerIdleState : State
    {
        public PlayerIdleState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
            player.ChangeState(actionRequestData.StateTypeEnum);
        }

        public override void SetMovementDir(Vector2 position)
        {
            base.SetMovementDir(position);
            if (IsMove){
                player.ChangeState(StateType.Move);
            }
        }


        public override void Enter()
        {
            m_Data.StateTypeEnum = StateType.Idle;
            player.serverplayer.NetState.RecvDoActionClientRPC(m_Data);

        }


        public override StateType GetId()
        {
            return StateType.Idle;
        }



    }
}

