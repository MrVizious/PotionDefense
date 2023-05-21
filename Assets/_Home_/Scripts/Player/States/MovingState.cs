using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.InputSystem;
using DesignPatterns;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingState : PlayerState
{
    private Rigidbody2D rb;
    public override void Enter(StateMachine<PlayerState> newStateMachine)
    {
        //Debug.Log("Entering moving state");
        base.Enter(newStateMachine);
        rb = playerController.rb;
    }

    public override void Dash()
    {
        DashingState dashingState = this.GetOrAddComponent<DashingState>();
        dashingState.direction = playerController.lastMovementInput.normalized;
        playerController.SubstituteStateWith(dashingState);
    }

    protected virtual void Update()
    {
        if (playerController.lastMovementInput.sqrMagnitude < 0.1f)
        {
            playerController.ChangeToState(this.GetOrAddComponent<IdleState>());
            return;
        }
    }

    protected virtual void FixedUpdate()
    {
        //rb.MovePosition((Vector2)rb.position + playerController.lastMovementInput * playerData.speed * Time.deltaTime);
        //Debug.Log(playerController.lastMovementInput);
        rb.velocity = playerController.lastMovementInput * playerData.speed;
    }

    public override void Exit()
    {
        rb.velocity = Vector2.zero;
        base.Exit();
    }
}
