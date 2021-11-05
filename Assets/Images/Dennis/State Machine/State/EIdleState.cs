using System.Collections;
using System.Collections.Generic;
using LF2.Server;
using UnityEngine;

public class EIdleState : AIState
{
    int pointMana;
    int distanceToPlayer;

    public EIdleState(AIBrainNew aIBrain, ServerCharacter player) : base(aIBrain, player)
    {

    }

    // public override void Enter()
    // {
    //     int ChanceToDoSomeThing = UnityEngine.Random.Range(1,3);
    //     pointMana = player.statsHealthSysteme.currentMana;
    //     // distanceToPlayer = Vector3.Distance(player.transform.position, playerPosition.GetPlayerPosition())
    // }


}
