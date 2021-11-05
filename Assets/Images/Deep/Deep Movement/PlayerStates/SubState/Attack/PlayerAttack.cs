using System.Collections;
using System.Collections.Generic;
using LF2.Server;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    // private List<IDamageable> dectectedDamageable = new List<IDamageable>();
    float attack12distance;

    public PlayerAttack(ServerCharacter player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    // Transform attackTransform ;


    public override void Enter()
    {

        

    }   

    public override void PhysicsUpdate()
    {
        // switch (stateMachine.CurrentState.attackType)
        // {
        //     case AttackType.Attack1:
        //         AttackToIdle();
        //         break;
        //     case AttackType.Attack3:
        //         AttackToIdle();
        //         break;
        //     case AttackType.Attack4:
        //         FlyAttackLand();
        //         break;
        //     case AttackType.Attack5:
        //         FlyAttackLand();
        //         break;
        // }
     
    }



    private void AttackToIdle()
    {
        // // TODO 
        // if (isAnimationFinished || isFinishedAnimation())
        // {
        //     isAnimationFinished = false;
        //     stateMachine.ChangeState(player.IdleState);
        // }
    }

    private void FlyAttackLand()
    {
        // if (player.CheckrGounded() && Mathf.Abs(player.currentVelocity.y) < 0.01f)
        // {
        //     stateMachine.ChangeState(player.LandState);
        // }
    }



}
