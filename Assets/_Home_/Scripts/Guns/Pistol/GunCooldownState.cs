using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using UtilityMethods;
using ExtensionMethods;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class GunCooldownState : GunState
{
    public override void Enter(StateMachine<GunState> newStateMachine)
    {
        base.Enter(newStateMachine);
        Debug.Log("Cooling down");
        UtilityMethods.UniTaskMethods.DelayedFunction(() => EndCooldown(), gunController.currentGun.gunData.cooldownSeconds).Forget();
    }

    private void EndCooldown()
    {
        if (gunController.currentGun.currentBullets > 0)
        {
            gunController.SubstituteStateWith(gunController.currentGunReadyState);
        }
        else
        {
            gunController.SubstituteStateWith(gunController.currentGunEmptyState);
        }
    }

    public override void Reload(InputAction.CallbackContext c)
    {
        if (!gunController.currentGun.CanReload()) return;

        gunController.SubstituteStateWith(gunController.currentGunReloadingState);
    }

    public override void Exit()
    {
        Destroy(this);
    }
}