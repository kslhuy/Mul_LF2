using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMovement : MonoBehaviour
{

    private Vector3 moveDir;
    public int FacingDirection{get ; private set;}

    [SerializeField]
    Rigidbody m_Rigidbody;    

    [SerializeField]
    BoxCollider m_BoxCollider; 
    private float JumpHieght = 9f ;
    private float JumpLength = 3f ;

    private int k_GroundLayerMask;


    private void Awake() {
        
        FacingDirection = 1;
        k_GroundLayerMask = LayerMask.GetMask(new[] { "Ground" });

    }

    #region Set Functions
         
    public void SetVelocityXZ(Vector3 targetposition){
        transform.position += targetposition*Time.deltaTime;
        CheckIfShouldFlip(Mathf.RoundToInt(targetposition.x));
    }

    /// <summary>
    /// Sets Jump
    /// </summary>
    /// <param name="position">Position in world space to path to. </param>
    public void SetJump(Vector3 moveDir){
        m_Rigidbody.AddForce(JumpHieght*Vector3.up + JumpLength*moveDir,ForceMode.Impulse);     
    }

    public void SetDoubleJump(Vector3 moveDir){
        m_Rigidbody.AddForce(JumpHieght*Vector3.up + (JumpLength+1)*FacingDirection*Vector3.right,ForceMode.Impulse); 
    }

    // public void SetFallingDown(){
    //     if ((m_Rigidbody.velocity.y) < 0f)
    //     {
    //         gaviti = m_gravity.Evaluate(Time.deltaTime);
    //         m_Rigidbody.velocity += gaviti * Physics.gravity.y * Vector3.up * Time.deltaTime;
    //     }
        
    // }

    public bool IsGounded(){
        bool hit_ground = Physics.Raycast(m_BoxCollider.bounds.center,Vector3.down ,m_BoxCollider.bounds.extents.y,k_GroundLayerMask);
        // Color rayColor;
        // if (!hit_ground){
        //     rayColor = Color.green;
        // }else {
        //     rayColor = Color.red;
        // }
        // Debug.DrawRay(m_BoxCollider.bounds.center , Vector3.down * (m_BoxCollider.bounds.extents.y),rayColor);

        return  hit_ground;
    }    



    // public void SetVelocityRun(float velocityRun ){
    //     moveDir.Set(velocityRun*FacingDirection ,0, 0);
    //     Rigidbody.velocity = moveDir;
    //     // transform.position += moveDir*Time.deltaTime;
    // }
    // public void SetFallingDown(){
    //     Rigidbody.velocity += 0.5f*Physics.gravity.y*Vector3.up*Time.deltaTime ;
    // }

    // public void SetVolocityDoubleJump( Vector3 velocitydoubleJump){
    //     moveDir.Set(velocitydoubleJump.x*FacingDirection ,velocitydoubleJump.y, velocitydoubleJump.z);
    //     Rigidbody.velocity = moveDir;
    // }


    // public void SetVelocitySliding(float GainDecreaseRunSpeed , float RunSpeed ){
    //     moveDir.Set(RunSpeed*FacingDirection,0,0);
    //     // Rigidbody.velocity = moveDir;
    //     Rigidbody.transform.position +=  moveDir * Time.deltaTime ;
    // }

    // public void SetVelocityRolling(float RollingSpeed  ){
    //     moveDir.Set(RollingSpeed*FacingDirection,0,0);
    //     Rigidbody.velocity = moveDir;

    //     // transform.position += FacingDirection  * moveDir * Time.deltaTime ;
    // }

    public void CheckIfShouldFlip(int xInput){
        if (xInput != 0 && xInput != FacingDirection){
            Flip();
        }
    }

    public void Flip(){
        FacingDirection *=-1;
        m_Rigidbody.transform.Rotate(0.0f,180.0f,0.0f);
    }

    #endregion
}
