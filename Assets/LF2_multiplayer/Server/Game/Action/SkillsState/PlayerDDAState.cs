using Unity.Netcode;
using UnityEngine;

namespace LF2.Server{

    public class PlayerDDAState : State
    {

        bool m_ExecutionFired;
        float m_MaxDistance = 0.35f;


        public PlayerDDAState(PlayerStateMachine player) : base(player)
        {
        }

        public override void CanChangeState(StateRequestData actionRequestData)
        {
           
        }

        public override void Enter()
        {      
            base.Enter();

            ComboLogic skillsDescription = player.SkillDescription(StateType.DDA).ComboLogic;
            switch (skillsDescription){
                case ComboLogic.LaunchProjectile :{
                    LaunchProjectile();
                    break;
                }   
            }
          
        }


        public override StateType GetId()
        {
            return StateType.DDA;
        }


        public override void End()
        {
            player.ChangeState(StateType.Idle);
        }

      
        /// <summary>
        /// Looks through the ProjectileInfo list and finds the appropriate one to instantiate.
        /// For the base class, this is always just the first entry with a valid prefab in it!
        /// </summary>
        /// <exception cref="System.Exception">thrown if no Projectiles are valid</exception>
        protected  SkillsDescription.ProjectileInfo GetProjectileInfo()
        {
            foreach (var projectileInfo in player.SkillDescription(StateType.DDA).Projectiles)
            {
                if (projectileInfo.ProjectilePrefab && projectileInfo.ProjectilePrefab.GetComponent<NetworkProjectileState>())
                    return projectileInfo;
            }
            throw new System.Exception($"Action {StateType.DDA} has no usable Projectiles!");
        }

        /// <summary>
        /// Instantiates and configures the arrow. Repeatedly calling this does nothing.
        /// </summary>
        /// <remarks>
        /// This calls GetProjectilePrefab() to find the prefab it should instantiate.
        /// </remarks>
        protected void LaunchProjectile()
        {

            var projectileInfo = GetProjectileInfo();
            var projectile = GameObject.Instantiate(projectileInfo.ProjectilePrefab, projectileInfo.ProjectilePrefab.transform.position, projectileInfo.ProjectilePrefab.transform.rotation);
            var playerPOS = player.serverplayer.physicsWrapper.Transform;
            
            // point the projectile the same way we're facing
            projectile.transform.right = playerPOS.right;
            var corrigePivot =   new Vector3((float)(playerPOS.right.x*0.4),0.36f,0);

            //this way, you just need to "place" the arrow by moving it in the prefab, and that will control
            //where it appears next to the player.

            projectile.transform.position = corrigePivot + playerPOS.localToWorldMatrix.MultiplyPoint(projectile.transform.position);
            projectile.GetComponent<ServerProjectileLogic>().Initialize(player.serverplayer.NetworkObjectId, in projectileInfo);

            projectile.GetComponent<NetworkObject>().Spawn();
        
        }





    }
}
