using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using DesignPatterns;
using Cysharp.Threading.Tasks;


public class GunReloadingState : GunState
{
    public override void Enter(StateMachine<GunState> newStateMachine)
    {
        base.Enter(newStateMachine);
        Debug.Log("Entering reload state");
        if (!gunController.currentGun.CanReload()) gunController.ChangeToPreviousState();
        UtilityMethods.UniTaskMethods.DelayedFunction(() => EndReload(), gunController.currentGun.gunData.reloadSeconds).Forget();
    }

    private void EndReload()
    {
        gunController.currentGun.Reload();
        gunController.SubstituteStateWith(gunController.currentGunReadyState);
    }
    public override void Exit()
    {
        Destroy(this);
    }
}
