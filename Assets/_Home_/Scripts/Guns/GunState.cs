using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UnityEngine.InputSystem;
using ExtensionMethods;

public abstract class GunState : MonoBehaviour, State<GunState>
{
    public GunController gunController
    {
        get
        {
            return ((GunController)stateMachine);
        }
    }
    public StateMachine<GunState> stateMachine
    {
        get;
        protected set;
    }

    public virtual void Shoot(InputAction.CallbackContext c) { }
    public virtual void Reload(InputAction.CallbackContext c) { }

    public virtual void Enter(StateMachine<GunState> newStateMachine)
    {
        stateMachine = newStateMachine;
        this.enabled = true;
    }

    public virtual void Exit()
    {
        Destroy(this);
    }
}
