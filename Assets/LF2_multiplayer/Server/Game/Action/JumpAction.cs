// using UnityEngine;

// namespace LF2.Server
// {
//     public class JumpAction : Action
//     {
//         private ServerCharacterMovement m_Movement;
//         private bool isLanded;

//         public JumpAction(ServerCharacter parent, ref ActionRequestData data) : base(parent, ref data)
//         {
//         }

//         public override bool Start()
//         {
//             m_Parent.NetState.RecvDoActionClientRPC(Data);

//             m_Movement = m_Parent.GetComponent<ServerCharacterMovement>();
//             m_Movement.SetJump(m_Data.Direction);
//             return true;
//         }

//         public override bool Update()
//         {
//             if (m_Movement.GetMovementState() == MovementState.Idle){
//                 isLanded = true;
//                 return  ActionConclusion.Stop;
//             }
//             // ChainIntoNewAction()
//             // Debug.Log("Still in loop JumpAction");
//             return ActionConclusion.Continue;
//         }

//         public override bool ChainIntoNewAction(ref ActionRequestData newAction)
//         {
            
//             if (isLanded)
//             {
//                 newAction = new ActionRequestData()
//                 {
//                     ActionTypeEnum = ActionType.Land,
//                     ShouldQueue = false,
//                 };
//                 return true;
//             }
//             return false;

            
//         }

//         public override bool WantToMoveNextAction(Action action)
//         {
//             if (action.Data.ActionTypeEnum == ActionType.AttackGeneral ){
//                 var newAction = new ActionRequestData()
//                 {
//                     ActionTypeEnum = ActionType.AttackGeneral,
//                     ShouldQueue = false,
//                 };
//                 return ChainIntoNewAction(ref newAction);
//             }
//             return false;
//         }
//     }
// }