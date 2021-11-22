// using UnityEngine;

// namespace LF2
// {
//     /// <summary>
//     /// Abstract base class containing some common members shared by Action (server) and ActionFX (client visual)
//     /// </summary>
//     public abstract class ActionBase
//     {
//         protected StateRequestData m_Data;

//         /// <summary>
//         /// Time when this Action was started (from Time.time) in seconds. Set by the ActionPlayer or ActionVisualization.
//         /// </summary>
//         public float TimeStarted { get; set; }

//         /// <summary>
//         /// How long the Action has been running (since its Start was called)--in seconds, measured via Time.time.
//         /// </summary>
//         public float TimeRunning { get { return (Time.time - TimeStarted); } }

//         /// <summary>
//         /// RequestData we were instantiated with. Value should be treated as readonly.
//         /// </summary>
//         public ref StateRequestData Data => ref m_Data;

//         /// <summary>
//         /// Data Description for this action.
//         /// </summary>
//         public ActionDescription Description
//         {
//             get
//             {
//                 ActionDescription result;
//                 var found = GameDataSource.Instance.ActionDataByType.TryGetValue(Data.StateTypeEnum, out result);
//                 Debug.AssertFormat(found, "Tried to find StateType %s but it was missing from GameDataSource!", Data.StateTypeEnum);

//                 return GameDataSource.Instance.ActionDataByType[Data.StateTypeEnum];
//             }
//         }


//         public bool AnimationActionTrigger;
//         public ActionBase(ref StateRequestData data)
//         {
//             m_Data = data;
//         }

//     }

// }
