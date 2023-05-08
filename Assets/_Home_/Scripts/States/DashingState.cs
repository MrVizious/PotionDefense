using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UtilityMethods;
using ExtensionMethods;

public class DashingState : PlayerState
{
    public Vector2 direction;
    private bool dashing = false;
    private Rigidbody2D rb;
    public override void Enter(StateMachine<PlayerState> newStateMachine)
    {
        base.Enter(newStateMachine);
        if (TryingToDashAgainstTouchingCollider())
        {
            playerController.ChangeToPreviousState();
            return;
        }
        rb = playerController.rb;
        Dashing().Forget();
        UniTaskMethods.DelayedFunction(() => StopDashing(), playerData.dashDuration).Forget();
    }

    private bool TryingToDashAgainstTouchingCollider()
    {
        // If the player is against one or more walls, check if they are trying to dash against them
        if (playerController.touchingColliders.Count <= 0)
        {
            return false;
        }
        LayerMask mask = ~LayerMask.GetMask("Player", "Camera", "Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 3f, mask);
        Collider2D hitCol = hit.collider;
        //Debug.DrawRay(transform.position, direction * 3f, Color.green, 2f);
        foreach (Collider2D col in playerController.touchingColliders)
        {
            if (col.Equals(hitCol))
            {
                return true;
            }
        }
        return false;
    }

    private async UniTask Dashing()
    {
        dashing = true;
        while (dashing)
        {
            rb.MovePositionInDirection(playerData.dashSpeed * direction * Time.fixedDeltaTime);
            await UniTask.WaitForFixedUpdate();
        }
    }

    public void StopDashing()
    {
        if (dashing == false) return;
        dashing = false;
        playerController.ChangeToPreviousState();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Pit"))
        {
            StopDashing();
        }
    }
}
