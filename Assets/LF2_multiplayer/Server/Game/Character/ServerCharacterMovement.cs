using System.Collections;
using MLAPI;
using UnityEngine;
using UnityEngine.Assertions;

namespace LF2.Server
{
    
    public enum MovementState
    {
        Idle = 0,
        // PathFollowing = 1,
        Move = 1,
        Charging = 2,
        Knockback = 3,
        Air = 4,
    }

    /// <summary>
    /// Component responsible for moving a character on the server side based on inputs.
    /// </summary>
    [RequireComponent(typeof(NetworkCharacterState), typeof(ServerCharacter)), RequireComponent(typeof(Rigidbody))]
    public class ServerCharacterMovement : NetworkBehaviour
    {
        [SerializeField] AnimationCurve m_gravity;
        [SerializeField]
        private float JumpSpeed = 10f;
        private Rigidbody m_Rigidbody;
        private BoxCollider m_BoxCollider;
        private NetworkCharacterState m_NetworkCharacterState;


        private MovementState m_MovementState;

        private ServerCharacter m_CharLogic;

        // when we are in charging and knockback mode, we use these additional variables
        private float m_ForcedSpeed;
        private float m_SpecialModeDurationRemaining;

        // this one is specific to knockback mode
        private Vector3 m_KnockbackVector;
        private float m_startTime;
        private Vector3 workSpace;
      

        public int FacingDirection { get; private set; }
        public bool DebugPlayer { get; private set; }

        public float  SpeedWalk = 1f ;
        public float gaviti = 1f ;
        private int k_GroundLayerMask;

        private void Awake()
        {
            m_NetworkCharacterState = GetComponent<NetworkCharacterState>();
            m_CharLogic = GetComponent<ServerCharacter>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_BoxCollider = GetComponent<BoxCollider>();
            FacingDirection = 1;
            m_MovementState = MovementState.Idle;
        }

        public override void NetworkStart()
        {
            if (!IsServer)
            {
                // Disable server component on clients
                enabled = false;
                return;
            }

            m_NetworkCharacterState.InitNetworkPositionAndRotationY(transform.position, transform.rotation.eulerAngles.y);
            k_GroundLayerMask = LayerMask.GetMask(new[] { "Ground" });


        }

        /// <summary>
        /// Sets a movement target. We will path to this position, .
        /// </summary>
        /// <param name="position">Position in world space to path to. </param>
        public void SetMovementTarget(Vector2 position)
        {
            workSpace.Set(position.x , 0, position.y);
            if (IsGounded()){
                if (position != Vector2.zero){
                    m_MovementState = MovementState.Move;    
                }  
                else {
                    m_MovementState = MovementState.Idle;
                }
            }
            
            // if ( !(m_MovementState == MovementState.Air)){
            //     if (position != Vector2.zero){
            //         m_MovementState = MovementState.Move;    
            //     }
            //     else m_MovementState = MovementState.Idle;
            // }

        }

        /// <summary>
        /// Sets Jump
        /// </summary>
        /// <param name="position">Position in world space to path to. </param>
        public void SetJump(Vector3 position)
        {
            if ( m_MovementState == MovementState.Air )   {return ; }   

            m_startTime = Time.time;
            m_MovementState = MovementState.Air;
            workSpace = position;
            
            m_Rigidbody.AddForce(JumpSpeed*workSpace,ForceMode.Impulse);

        }

        public void StartForwardCharge(float speed, float duration)
        {
            m_MovementState = MovementState.Charging;
            m_ForcedSpeed = speed;
            m_SpecialModeDurationRemaining = duration;
        }

        public void StartKnockback(Vector3 knocker, float speed, float duration)
        {
            m_MovementState = MovementState.Knockback;
            m_KnockbackVector = transform.position - knocker;
            m_ForcedSpeed = speed;
            m_SpecialModeDurationRemaining = duration;
        }

        public bool IsGounded(){
            bool hit_ground = Physics.Raycast(m_BoxCollider.bounds.center,Vector3.down ,m_BoxCollider.bounds.extents.y,k_GroundLayerMask);
            Color rayColor;
            if (!hit_ground){
                rayColor = Color.green;
            }else {
                rayColor = Color.red;
            }
            Debug.DrawRay(m_BoxCollider.bounds.center , Vector3.down * (m_BoxCollider.bounds.extents.y),rayColor);

            return  hit_ground;
        }    


        // private void OnDrawGizmos() {
        // if (DebugPlayer){
        //     // Gizmos.DrawSphere(AttackTransform.position,PlayerData.attackRadius);
        //     // Gizmos.DrawCube(m_BoxCollider.bounds.center,boxCollider.bounds.extents);
        //     // Gizmos.DrawLine(m_BoxCollider.bounds.center,m_BoxCollider.bounds.extents);

        // // Debug.Log(hit_ground);
        // }
        // }
        


        /// <summary>
        /// Returns true if the current movement-mode is unabortable (e.g. a knockback effect)
        /// </summary>
        /// <returns></returns>
        public bool IsPerformingForcedMovement()
        {
            return m_MovementState == MovementState.Knockback || m_MovementState == MovementState.Charging;
        }

        /// <summary>
        /// Returns true if the character is actively moving, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsMoving()
        {
            return m_MovementState != MovementState.Idle;
        }

        /// <summary>
        /// Cancels any moves that are currently in progress.
        /// </summary>
        public void CancelMove()
        {
            m_MovementState = MovementState.Idle;
        }

        /// <summary>
        /// Instantly moves the character to a new position. NOTE: this cancels any active movement operation!
        /// This does not notify the client that the movement occurred due to teleportation, so that needs to
        /// happen in some other way, such as with the custom action visualization in DashAttackActionFX. (Without
        /// this, the clients will animate the character moving to the new destination spot, rather than instantly
        /// appearing in the new spot.)
        /// </summary>
        /// <param name="newPosition">new coordinates the character should be at</param>
        public void Teleport(Vector3 newPosition)
        {
            CancelMove();


            m_Rigidbody.position = transform.position;
            m_Rigidbody.rotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            PerformMovement();
            // Send new position values to the client
            m_NetworkCharacterState.NetworkPosition.Value = transform.position;
            m_NetworkCharacterState.NetworkRotationY.Value = transform.rotation.eulerAngles.y;
            m_NetworkCharacterState.NetworkMovementSpeed.Value = GetMaxMovementSpeed();
            m_NetworkCharacterState.MovementStatus.Value = GetMovementStatus();
        }


        private void OnDestroy()
        {

        }

        private void PerformMovement()
        {
 
            switch (m_MovementState){
                case MovementState.Idle:
                    break;

                case MovementState.Knockback:
                    break;
                case MovementState.Charging:
                    break;

                case MovementState.Move:
                    transform.position += workSpace*Time.deltaTime*SpeedWalk;
                    CheckIfShouldFlip((int)Mathf.Sign(workSpace.x));
                    break;
                case MovementState.Air:
                    if ((m_Rigidbody.velocity.y) < 0f)  {
                        SetFallingDown();
                    }             
                    if (IsGounded() &&   Time.time - m_startTime > 0.2f ){
                       m_MovementState = MovementState.Idle;
                    }       

                    // Debug.Log(m_Rigidbody.velocity.y);
                    break;
            }

            // Debug.Log(m_MovementState);
            

        }


        public void SetFallingDown()
        {
            gaviti = m_gravity.Evaluate(Time.deltaTime);
            m_Rigidbody.velocity += gaviti * Physics.gravity.y * Vector3.up * Time.deltaTime;
        }

        public void SetMovementState(MovementState movementState){
            m_MovementState = movementState;
        }
        public MovementState GetMovementState(){
            
            return m_MovementState;

        }


        private float GetMaxMovementSpeed()
        {
            switch (m_MovementState)
            {
                case MovementState.Charging:
                case MovementState.Knockback:
                    return m_ForcedSpeed;
                case MovementState.Idle:
                case MovementState.Move:
                default:
                    return GetBaseMovementSpeed();
            }
        }

        /// <summary>
        /// Retrieves the speed for this character's class.
        /// </summary>
        private float GetBaseMovementSpeed()
        {
            CharacterClass characterClass = GameDataSource.Instance.CharacterDataByType[m_CharLogic.NetState.CharacterType];
            Assert.IsNotNull(characterClass, $"No CharacterClass data for character type {m_CharLogic.NetState.CharacterType}");
            return characterClass.Speed;
        }

        /// <summary>
        /// Determines the appropriate MovementStatus for the character. The
        /// MovementStatus is used by the client code when animating the character.
        /// </summary>
        private MovementStatus GetMovementStatus()
        {
            switch (m_MovementState)
            {
                case MovementState.Move:
                    return MovementStatus.Move;
                case MovementState.Knockback:
                    return MovementStatus.Uncontrolled;
                case MovementState.Air:
                    return MovementStatus.Air;
                default:
                    return MovementStatus.Idle;
            }
        }
        public void CheckIfShouldFlip(int xInput){
            if (xInput != 0 && xInput != FacingDirection){
                Flip();
            }
        }
        public void Flip(){
            FacingDirection *=-1;
            transform.Rotate(0.0f,180.0f,0.0f);
        }
    }
}
