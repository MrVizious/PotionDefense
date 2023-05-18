using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class LevelEndedState : LevelState
{
    public override void Enter(StateMachine<LevelState> newStateMachine)
    {
        base.Enter(newStateMachine);
        Debug.Log("Level ended!");
    }
}
