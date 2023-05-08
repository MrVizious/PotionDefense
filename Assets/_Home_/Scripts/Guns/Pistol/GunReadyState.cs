using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ExtensionMethods;
using DesignPatterns;

public class GunReadyState : GunState
{
    public override void Enter(StateMachine<GunState> newStateMachine)
    {
        base.Enter(newStateMachine);
        Debug.Log("Entering ready state");
        if (gunController.currentGun.currentBullets <= 0)
        {
            if (gunController.currentGun.extraBullets <= 0)
            {
                gunController.ChangeToState(gunController.currentGunEmptyState);
            }
            else
            {
                gunController.ChangeToState(gunController.currentGunReloadingState);
            }
        }
    }
    public override void Shoot(InputAction.CallbackContext c)
    {
        if (!c.started) return;
        if (gunController.currentGun.gunData.hasCharging)
        {
            gunController.ChangeToState(gunController.currentGunChargingState);
        }
        else
        {
            gunController.ChangeToState(gunController.currentGunShootingState);
        }

    }

    public override void Reload(InputAction.CallbackContext c)
    {
        if (gunController.currentGun.currentBullets >= gunController.currentGun.gunData.magazineSize) return;
        gunController.ChangeToState(gunController.currentGunReloadingState);
    }

    public override void Exit()
    {
        this.enabled = false;
    }
}