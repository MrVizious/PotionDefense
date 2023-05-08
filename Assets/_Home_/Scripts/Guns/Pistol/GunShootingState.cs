using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using ExtensionMethods;

public class GunShootingState : GunState
{
    public override void Enter(StateMachine<GunState> newStateMachine)
    {
        base.Enter(newStateMachine);
        gunController.currentGun.currentBullets--;
        Debug.Log("Pium");
        if (gunController.currentGun.gunData.hasCooldown)
        {
            gunController.SubstituteStateWith(gunController.currentGunCooldownState);
        }
        else
        {
            gunController.SubstituteStateWith(gunController.currentGunReadyState);
        }
    }

    public override void Exit()
    {
        Destroy(this);
    }
}
