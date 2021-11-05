using System.Collections;
using System.Collections.Generic;
using LF2.Server;
using UnityEngine;
public class EMoveState : AIState
{
    // 
    private float distanceCanAttack;

    protected float distanceCanRun = 3f;
    private float maxTime;

    public EMoveState(AIBrainNew aIBrain, ServerCharacter player) : base(aIBrain, player)
    {
        // distanceCanAttack = player.PlayerData.maxDistance;
        // playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<IPlayerPosition>();
    }

    protected IPlayerPosition playerPosition{get; private set;} 




    public override void Enter()
    {
        base.Enter();
    }


    // public override void LogicUpdate()
    // {
    //     base.LogicUpdate();
    //     // ChasePlayerNavMesh();
    //     // AttackPlayer();
    //     if (playerPosition.IsTargetable()){

    //         if (Vector3.Distance(player.transform.position, playerPosition.GetPlayerPosition()) < distanceCanAttack)
    //         {
    //             // to close : Stop and Attack or do something 
                
    //             aIBrain.ChangeAIState(aIBrain.EAttackState);

    //         } else if (Vector3.Distance(player.transform.position, playerPosition.GetPlayerPosition()) > distanceCanRun){
    //             // Jump or Run (random)
    //             // int run = UnityEngine.Random.Range(1,3);
    //             int run = 2;
    //             if (run >= 2) aIBrain.ChangeAIState(aIBrain.ERunState);
    //             // else stateMachine.ChangeState(player.EJumpState);
    //         }
            
    //         else
    //         {
    //             Vector3 targetDir = (playerPosition.GetPlayerPosition() - player.transform.position ).normalized;
                
    //             // Too  far : continue
    //             player.Core.SetMovement.SetVelocityXZ( targetDir.x, targetDir.z);
    //             player.Core.SetMovement.CheckIfShouldFlip((int)(Mathf.Sign(targetDir.x)));
    //         }
    //     }
    //     else {
    //         // TODO : path Finding ? 
    //     }

    // }

    // private void AttackPlayer()
    // {
    //     float AttackRange = 0.5f;
    //     if (Vector3.Distance(player.transform.position, playerPosition.GetPlayerPosition()) < AttackRange)
    //     {
    //         aIBrain.ChangeAIState(aIBrain.EAttackState);
    //     }

    // }

    // private void ChasePlayerNavMesh()
    // {
    //     if (!player.agentNavMesh.enabled)
    //     {
    //         return;
    //     }

    //     timer -= Time.deltaTime;
    //     if (!player.agentNavMesh.hasPath)
    //     {
    //         player.agentNavMesh.destination = player.PlayerTransform.position;
    //     }

    //     if (timer < 0.0f)
    //     {
    //         float sqdistance = (player.PlayerTransform.position - player.agentNavMesh.destination).sqrMagnitude;
    //         if (sqdistance > maxDistance * maxDistance)
    //         {
    //             player.agentNavMesh.destination = player.PlayerTransform.position;
                
    //                         player.core.SetMovement.CheckIfShouldFlip((int)(player.agentNavMesh.desiredVelocity.x));
    //         }
    //         timer = maxTime;
    //     }
    // }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
