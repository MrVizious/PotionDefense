using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public abstract class LevelState : MonoBehaviour, State<LevelState>
{
    public StateMachine<LevelState> stateMachine
    {
        get;
        protected set;
    }

    public virtual void Enter(StateMachine<LevelState> newStateMachine) { }

    public virtual void Exit() { }
}
