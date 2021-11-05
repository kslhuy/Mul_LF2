using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    // public PlayerStateMachine StateMachine{ get; private set;}
    
    #region Player States 
    // public PlayerIdleState IdleState{get; private set;}
    // public PlayerMoveState MoveState{get; private set;}

    // public PlayerJumpState JumpState{get ; private set;}


    // public PlayerLandState LandState{get;private set;}
    // public PlayerAirState AirState{get; private set;}

    // public PlayerDoubleJumpState DoubleJumpState{get ; private set;}

    // public PlayerRunState RunState{get; private set;}
    // // public RunSliding SlidingState{get; private set;}

    // public PlayerAttack12 AttackState12{get;private set;}

    // public PlayerDefenseState DefenseState{get;private set;}

    // public PlayerRollingState RollingState {get; private set;}




    #endregion

    
    // private AIBrain _aiBrain;


 

    #region Common States
    // public PlayerHurtState HurtState {get; private set;} 

    // public DieState DieState {get; private set;}   
    #endregion

    #region Components
    
    public Core Core {get; private set;}
    public PlayerInputHandler_V2 InputHandler{get;private set;}
    public Animation_Base AnimationBase {get ; private set;}
    public Rigidbody Rigidbody{ get; private set;}
    public BoxCollider boxCollider{get;private set;}
         
    #endregion
    
    #region Check Transforms
    public Transform AttackTransform;

    #endregion

    #region Other Variables
    // General Data for one character 
    [SerializeField] private PlayerData _playerData;

    public PlayerData PlayerData{
        get{ return _playerData;}
    }

    // need modify
    public Vector3 currentVelocity{get; private set;}

    [SerializeField] private bool isNPC;
    public bool IsNPC{
        get {return isNPC;}
    } 

    public StatsHealthSysteme statsHealthSysteme{get; private set;}

    #endregion


    #region AnimationToHash

    int idle = Animator.StringToHash("Idle_anim");
    public int Idle {get => idle;}

    int walk = Animator.StringToHash("Walk_anim");
    public int Walk {get => walk;}


    int jump = Animator.StringToHash("Jump_anim");
    public int Jump {get => jump;}


    int doubleJump = Animator.StringToHash("DoubleJump_anim");
    public int DoubleJump {get => doubleJump;}

    int doubleJump2 = Animator.StringToHash("DoubleJump2_anim");
    public int DoubleJump2 {get => doubleJump2;}


    int land = Animator.StringToHash("Land_anim");
    public int Land {get => land;}


    int Air = Animator.StringToHash("Air_anim");
    [HideInInspector]
    public int Run = Animator.StringToHash("Run_anim");
    [HideInInspector]
    public int Sliding = Animator.StringToHash("Sliding_anim");
    private int attack1=  Animator.StringToHash("Attack1_anim") ;
    public int Attack1 {
        get {return attack1;}
    }

    int attack2 = Animator.StringToHash("Attack2_anim") ;
    public int Attack2 {get {return attack2;}}

    int attack3 = Animator.StringToHash("Attack3_anim") ;
    public int Attack3 {get {return attack3;}}

    int attack4 = Animator.StringToHash("Attack4_anim") ;
    public int Attack4 {get {return attack4;}}
    int attack5 = Animator.StringToHash("Attack5_anim") ;
    [HideInInspector]
    public int Attack5 {get {return attack5;}}

    [HideInInspector]
    public int Defense = Animator.StringToHash("Defense_anim") ;
    [HideInInspector]
    public int Rolling = Animator.StringToHash("Rolling_anim") ;

    [HideInInspector]
    public int Hurt1 = Animator.StringToHash("Hurt1_anim");
    [HideInInspector]
    public int Hurt2 = Animator.StringToHash("Hurt2_anim");
    [HideInInspector]
    public int Hurt3 = Animator.StringToHash("Hurt3_anim");
    [HideInInspector]
    public int Hurt3Contre = Animator.StringToHash("Hurt3Control_anim");


             
    #endregion
    [SerializeField] private bool DebugPlayer ;
    
#region Event
    // Animation
    public Action finishAnimEvent;

    // Health
    // public event Action ModifierHealth;
    // protected Health health;
    // private HealthBar healthBar;
     
#endregion

    #region Unity CallBack Function

    protected virtual void Awake() {

        Core = GetComponentInChildren<Core>();
        AnimationBase = GetComponentInChildren<Animation_Base>();

        InputHandler = GetComponent<PlayerInputHandler_V2>();
        Rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        statsHealthSysteme = GetComponent<StatsHealthSysteme>();

        // StateMachine = new PlayerStateMachine();


        // Debug.Log(StateMachine);
        
    }

    protected virtual void Start() {

        
        // For AI 
        // if (IsNPC){
        //     _aiBrain = new AIBrain(this);
        // }

        // For player 
        // else{
        //     IdleState =  new PlayerIdleState(this , StateMachine , PlayerData,Idle);
        //     MoveState =  new PlayerMoveState(this , StateMachine , PlayerData,Walk);
        //     AirState = new PlayerAirState(this , StateMachine , PlayerData, Air);
        //     LandState = new PlayerLandState(this , StateMachine , PlayerData,Land);
        //     DoubleJumpState = new PlayerDoubleJumpState(this , StateMachine , PlayerData,DoubleJump);
        //     RunState = new PlayerRunState(this , StateMachine,PlayerData,Run);
        //     RollingState = new PlayerRollingState (this , StateMachine,PlayerData,Rolling);

        //     AttackState12 = new PlayerAttack12(this , StateMachine,PlayerData,Attack1);
        //     DefenseState =  new PlayerDefenseState (this , StateMachine,PlayerData,Defense);
        //     JumpState = new PlayerJumpState(this , StateMachine , PlayerData, Jump);
        //     HurtState = new PlayerHurtState(this , StateMachine,PlayerData,Hurt1);
        //     SlidingState = new RunSliding(this , StateMachine,PlayerData,Sliding);

        //     DieState = new DieState(this , StateMachine,PlayerData,Hurt3);
            // StateMachine.Initialize(IdleState);
        // }


        
    }



    protected virtual void Update() {
        currentVelocity = Rigidbody.velocity;
        if (IsNPC){
            // _aiBrain.currentState.LogicUpdate();
        }
        else{
            // StateMachine.CurrentState.LogicUpdate();
        }
        // Debug.Log(playerPosition.GetPlayerPosition());
        // Debug.Log(StateMachine.CurrentState);
    
    }
    protected virtual void FixedUpdate(){
        if (IsNPC){
            // _aiBrain.currentState.PhysicsUpdate();
        }
        else{
            // StateMachine.CurrentState.PhysicsUpdate();
        }
    }
    #endregion
    

    #region CheckFunction
    public bool CheckrGounded(){
        return  Physics.Raycast(boxCollider.bounds.center,Vector3.down ,boxCollider.bounds.extents.y,PlayerData.whatIsGround);
    }    
    #endregion


    // public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger(); 




    private void OnDrawGizmos() {
        if (DebugPlayer){
            Gizmos.DrawSphere(AttackTransform.position,PlayerData.attackRadius);
            // Gizmos.DrawCube(boxCollider.bounds.center,boxCollider.bounds.extents);
        }
    }

    // public void Attack(){
    //     Collider[] HitDected = Physics.OverlapSphere(AttackTransform.position,PlayerData.attackRadius,PlayerData.whatIsEnemy);
    //     foreach (Collider item in HitDected)
    //     {
    //         IDamageable damageable = item.GetComponent<IDamageable>();
    //         // item.GetComponent<IDamageable>();
    //         Debug.Log(damageable);
    //         if (damageable != null)
    //         {
    //             damageable.ReceiveHP(this,10);
    //         }
    //     }
    // }

    public void ReceiveHP(Player inflicter, int HP)
    {
        
    
    //to our own effects, and modify the damage or healing as appropriate. But in this game, we just take it straight.
            if (HP > 0)
            {

                return;
            }
            else
            {
                // StateMachine.ChangeState(HurtState);
                if (IsNPC){
                    // _aiBrain.ChangeAIState(_aiBrain.EHurtState);
                }
                statsHealthSysteme.SpendHealth(HP);
            }

    }
}
