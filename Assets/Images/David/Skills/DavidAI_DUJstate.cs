// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DavidAI_DUJstate : DUJstate
// {

//     // Creat Projectile 
//     SkillsData deep;
//     private int hashID;

//     public DavidAI_DUJstate(Player player, PlayerStateMachine stateMachine, PlayerData playerData, int hashID , SkillsData deep) : base(player, stateMachine, playerData, hashID)
//     {
//         this.deep = deep;
//         this.hashID = hashID;
//     }



//     public override void Enter()
//     {
//         base.Enter();
//         // player.AnimationBase.EnableProjectilEvent += CreateProjectile;
//     }



//     public override void Exit()
//     {
//         base.Exit();
//         // player.AnimationBase.EnableProjectilEvent -= CreateProjectile;

//     }



//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         // if(isFinishedAnimation()){
//         //     stateMachine.ChangeState(player.IdleState);
//         // }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }

//     // public void CreateProjectile(){
//     //     DeepDLA.Create(player.AttackTransform.position,core.SetMovement.FacingDirection*Vector3.right,deep.CharacterType);
//     // }


// }
