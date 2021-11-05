using System.Collections;
using System.Collections.Generic;
using LF2.Server;
using UnityEngine;

public class ERunState : EMoveState
{
    private int animationID;
    private float runVelocity;

    public ERunState(AIBrainNew aIBrain, ServerCharacter player) : base(aIBrain, player)
    {
        // animationID= IDanim;
        // runVelocity = player.PlayerData.runVelocity;
    }



    public override void Enter()
    {
        // player.AnimationBase.Anim.Play(animationID);
    }

    public override void LogicUpdate()
    {
        // player.Core.SetMovement.SetVelocityRun(runVelocity);
        
    //     if (Vector3.Distance(player.transform.position, playerPosition.GetPlayerPosition()) < distanceCanRun){
    //         aIBrain.ChangeAIState(aIBrain.ESlidingState);
    //     }
    }
}
