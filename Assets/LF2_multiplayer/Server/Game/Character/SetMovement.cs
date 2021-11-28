using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovement : MonoBehaviour
{

    private Vector3 moveDir;
    public int FacingDirection{get ; private set;}
    public Rigidbody Rigidbody{get;private set;}

    private void Awake() {
        
        Rigidbody = GetComponent<Rigidbody>();
        FacingDirection = 1;
    }

    #region Set Functions
         
    public void SetVelocityXZ(Vector3 targetposition){
        // moveDir = targetposition;
        // // Debug.Log (moveDir);
        // // Rigidbody.velocity = moveDir;
        transform.position += targetposition*Time.deltaTime;
    }

    internal void SetVelocityY(Vector3 velocityy)
    {
        moveDir.Set(velocityy.x*FacingDirection ,velocityy.y, 0);
        Rigidbody.velocity = moveDir ;
    }

    public void SetVelocityJump(float velocityy , Vector3 moveDir){
        Rigidbody.velocity = Vector3.up*velocityy + moveDir ;
    }

    public void SetVelocityRun(float velocityRun ){
        moveDir.Set(velocityRun*FacingDirection ,0, 0);
        Rigidbody.velocity = moveDir;
        // transform.position += moveDir*Time.deltaTime;
    }
    public void SetFallingDown(){
        Rigidbody.velocity += 0.5f*Physics.gravity.y*Vector3.up*Time.deltaTime ;
    }

    public void SetVolocityDoubleJump( Vector3 velocitydoubleJump){
        moveDir.Set(velocitydoubleJump.x*FacingDirection ,velocitydoubleJump.y, velocitydoubleJump.z);
        Rigidbody.velocity = moveDir;
    }


    public void SetVelocitySliding(float GainDecreaseRunSpeed , float RunSpeed ){
        moveDir.Set(RunSpeed*FacingDirection,0,0);
        // Rigidbody.velocity = moveDir;

        Rigidbody.transform.position +=  moveDir * Time.deltaTime ;
    }

    public void SetVelocityRolling(float RollingSpeed  ){
        moveDir.Set(RollingSpeed*FacingDirection,0,0);
        Rigidbody.velocity = moveDir;

        // transform.position += FacingDirection  * moveDir * Time.deltaTime ;
    }

    public void CheckIfShouldFlip(int xInput){
        if (xInput != 0 && xInput != FacingDirection){
            Flip();
        }
    }

    public void Flip(){
        FacingDirection *=-1;
        Rigidbody.transform.Rotate(0.0f,180.0f,0.0f);

    }


    #endregion
}
