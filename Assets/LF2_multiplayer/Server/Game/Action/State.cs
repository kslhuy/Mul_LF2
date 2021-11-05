using System.Collections.Generic;
using UnityEngine;
namespace LF2.Server
{
    public enum State
    {
        BlockState,
        NormalState,
        
    }

    public enum GameplayActivity
    {
        AttackedByEnemy,
        Healed,
        StoppedChargingUp,
        UsingAttackAction, // called immediately before we perform the attack Action
    }
}