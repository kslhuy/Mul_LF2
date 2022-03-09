using UnityEngine;

namespace LF2.Server
{
    public class Idle : AIState
    {
        private bool loadTarget_onetime;

        public Idle(AIBrain aIBrain) : base(aIBrain)
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Update()
        {
            // while idle, we are scanning for jerks to hate
            if (!loadTarget_onetime){
                loadTarget_onetime = true;
                DetectFoes();
            }

        }

        protected void DetectFoes()
        {

            // in this game, NPCs only attack players (and never other NPCs), so we can just iterate over the players to see if any are nearby
            foreach (var character in PlayerServerCharacter.GetPlayerServerCharacters())
            {
                if (m_Brain.IsAppropriateFoe(character) )
                {
                    m_Brain.Hate(character);
                }
            }
        }


        public override void FindTheNextState()
        {
        }

    }
}
