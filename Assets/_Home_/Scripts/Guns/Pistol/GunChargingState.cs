using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunChargingState : GunState
{
    private float timeCharged;
    public bool isCharged => timeCharged >= gunController.currentGun.gunData.chargeSeconds;
    public override void Shoot(InputAction.CallbackContext c)
    {
        // If the button was released (onButtonUp)
        if (c.canceled)
        {
            if (isCharged) { gunController.SubstituteStateWith(gunController.currentGunShootingState); }
            else
            {
                gunController.ChangeToPreviousState();
            }
        }
    }

    private void Update()
    {
        timeCharged += Time.deltaTime;
    }
}
