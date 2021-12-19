using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;



public class PlayerInputHandler_V2 : MonoBehaviour
{
    #region Movement
         
    public Vector2 RawMovementInput { get ; private set;}

    #endregion
    // JUMP
    public bool JumpInput{get;private set;}
    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    // JUMP

    //RUN
    private float lastHoldRightTime;
    private float lastHoldLeftTime;
    public bool canRun {get ; private set;}
    private int countTime;
    //RUN


    public bool AttackInput{get;private set;}

    public bool DefenseInput{get;private set;}

    //COMBO  
    // public KeyPress currentKeyPress{get;private set;}
    // public List<KeyPress> currentCombo = new List<KeyPress>();
    // public List<ComboAttack> avilableSkills;
 

    public event Action<TypeSkills> ComboTrigger;


    // COMBO

    private void Awake(){
        var classJumpCombo = GameObject.FindGameObjectWithTag("JumpUI").GetComponent<JumpButton>();            
        var classAttackCombo = GameObject.FindGameObjectWithTag("AttackUI").GetComponent<AttackButton>();            

        classJumpCombo.classJumpComboEvent += PerformCombo;
        classAttackCombo.classAttackComboEvent += PerformCombo;

        // NGU , only need is run or not , dont need direction
        var runLeftButton = GameObject.FindObjectOfType<RunLeftButton>().GetComponent<RunLeftButton>();            
        var runRightButton = GameObject.FindObjectOfType<RunRightButton>().GetComponent<RunRightButton>();           

        runLeftButton.runLeftEvent += GoRun;
        runRightButton.runRightEvent += GoRun;

    }

    private void GoRun()
    {
        canRun = true;
    }

    private void PerformCombo(TypeSkills typeCombo)
    {

        ComboTrigger?.Invoke(typeCombo);
    }

    private void Start(){
    }


    private void Update()
    {
        CheckJumpInutHoldTime();
    }



    public void OnMoveInput(InputAction.CallbackContext context){
        RawMovementInput = context.ReadValue<Vector2>();

        // NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        // NormInputZ = (int)(RawMovementInput * Vector2.down).normalized.y;
        
    }

    public void OnJumpInput(InputAction.CallbackContext context){
        if (context.started){
            JumpInput = true;
            jumpInputStartTime = Time.time;

        }
    }
    
    public void OnAttackInput(InputAction.CallbackContext context){
        if (context.started){
            AttackInput = true;
        }
        if (context.canceled){
            AttackInput = false;
        }
    }

    public void OnDefenseInput(InputAction.CallbackContext context){
        if (context.started){
            DefenseInput = true;
        }
        if (context.canceled){
            DefenseInput = false;
        }
    }


    public void UseJumpInput() => JumpInput = false;


    private void CheckJumpInutHoldTime(){
        if ( Time.time >= jumpInputStartTime + inputHoldTime){
            JumpInput = false;
        }
    }

    public void ResetRun(){
        canRun = false;
    }




}
