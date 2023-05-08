using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ExtensionMethods;

public class GunEmptyState : GunState
{
    public override void Shoot(InputAction.CallbackContext c)
    {
        if (gunController.currentGun.extraBullets > 0)
        {
            gunController.SubstituteStateWith(gunController.currentGunReloadingState);
        }
        /*
            else{
                //TODO: play sound
            }
        */
    }
    public override void Reload(InputAction.CallbackContext c)
    {
        if (!gunController.currentGun.CanReload()) return;
        gunController.SubstituteStateWith(gunController.currentGunReloadingState);
    }

    public override void Exit()
    {
        this.enabled = false;
    }
}
