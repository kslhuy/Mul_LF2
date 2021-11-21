// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;


// public class DavidAI : Player{

//     [SerializeField] SkillsData skillsData;

//     int DDA = Animator.StringToHash("D_L_A_anim");
//     int DDJ = Animator.StringToHash("D_L_J_anim");

    
//     // int DDA = Animator.StringToHash("D_D_A_anim");

//     int DUA = Animator.StringToHash("D_U_A_anim");
//     int DUJ = Animator.StringToHash("D_U_J_anim");

//     // public DavidAI_DDAstate E_DDAstate{get; private set;}
//     // public DavidAI_DUAstate E_DUAstate{get; private set;}

//     // public DavidAI_DDJstate E_DDJstate{get; private set;}
//     // public DavidAI_DUJstate E_DUJstate{get; private set;} 


 
//     protected override void Awake(){

//         base.Awake();
//         // E_DUAstate = new DavidAI_DUAstate(this , StateMachine,PlayerData,DUA,skillsData);
//         // // ban chuong
//         // E_DDAstate = new DavidAI_DDAstate(this , StateMachine,PlayerData,DDA,skillsData);
//         // // Lon :)) 
//         // E_DUJstate = new DavidAI_DUJstate(this , StateMachine,PlayerData,DUJ,skillsData);
//         // // chem lien hoan
//         // E_DDJstate = new DavidAI_DDJstate(this , StateMachine,PlayerData,DDJ,skillsData);

//     }
//     protected override void Start()
//     {
//         base.Start();
//         // statsHealthSysteme.DeadEvent += Dead; 
//     }



//     // public void Dead(){
//     //     StateMachine.ChangeState(DieState);
//     //     //Neu co lenh hoi sinh thi ko can unsub
//     //     // statsHealthSysteme.DeadEvent -= Dead; 

//     // }

    




    
    

    

    
// }