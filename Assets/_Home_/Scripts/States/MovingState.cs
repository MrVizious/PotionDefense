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

    private void Update()
    {
        if (playerController.lastMovementInput.sqrMagnitude < 0.1f)
        {
            playerController.ChangeToState(this.GetOrAddComponent<IdleState>());
            return;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)rb.position + playerController.lastMovementInput * playerData.speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Pit"))
        {
        }
    }
}
