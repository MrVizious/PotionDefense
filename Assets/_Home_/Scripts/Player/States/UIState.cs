using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.InputSystem;
using DesignPatterns;

[RequireComponent(typeof(Rigidbody2D))]
public class UIState : PlayerState
{
    private PlayerInput playerInput;
    public override void Enter(StateMachine<PlayerState> newStateMachine)
    {
        base.Enter(newStateMachine);
        playerInput = FindObjectOfType<PlayerInput>();
        if (!playerInput.currentControlScheme.ToLower().Equals("keyboard and mouse"))
        {
            playerLook.target.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public override void Exit()
    {
        playerLook.target.GetComponentInChildren<SpriteRenderer>().enabled = true;
        base.Exit();
    }


    public override void Shoot(InputAction.CallbackContext c) { }
}
