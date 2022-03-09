
namespace LF2.Server
{

    /// <summary>
    /// Base class for all AIStates
    /// </summary>
    public abstract class AIState
    {

        protected AIBrain m_Brain;

        public AIState(AIBrain aIBrain)
        {
            m_Brain = aIBrain;
        }



        /// <summary>
        /// Called once each time this state becomes the active state.
        /// (This will only happen if IsEligible() has returned true for this state)
        /// </summary>
        public abstract void OnEnter();

        public abstract void OnExit();

        /// <summary>
        /// Called once per frame while this is the active state. Initialize() will have
        /// already been called prior to Update() being called
        /// </summary>
        public abstract void Update();

        public abstract void FindTheNextState();

    }
}
