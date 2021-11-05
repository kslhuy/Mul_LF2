using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Base : MonoBehaviour
{
    public Animator Anim {get ; private set;}

    public Player player{get; private set;}

    public Action EnableProjectilEvent;


    private void Awake() {
        Anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void AnimationTriggerAttack()
    {
        #region Raycast not use
             
        // RaycastHit[]HitDectecd = Physics.BoxCastAll(boxCollider.bounds.center,boxCollider.bounds.extents, Vector3.right,Quaternion.identity, playerData.whatIsEnemy) ;

        // foreach (RaycastHit item in HitDectecd)
        // {
        //     IDamageable damageable = item.collider.GetComponent<IDamageable> ();
        //     // item.GetComponent<IDamageable>();
        //     Debug.Log(damageable);
        //     if (damageable != null)
        //     {
        //         damageable.Damage(10f);
        //     }
        // }
        #endregion
        
        // player.Attack();
        
    }

    // private void AnimationFinishTrigger(){
    //     player.AnimationFinishTrigger();
    // }

    // private void EnableProjectile(){
    //     EnableProjectilEvent?.Invoke();
    // }




}
