using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunInstance
{
    public GunData gunData;
    public int currentBullets;
    public int extraBullets;

    public GunInstance(GunData gun, int currentBullets = -1, int extraBullets = -1)
    {
        this.gunData = gun;
        // Use magazine size as bullet count if there is none specified
        this.currentBullets = currentBullets < 0 ? gun.magazineSize : currentBullets;
        this.currentBullets = extraBullets < 0 ? gun.magazineSize : extraBullets;
    }

    public void Reload()
    {
        if (!CanReload()) return;
        int desiredBulletsToReload = gunData.magazineSize - currentBullets;
        int actualBulletsToReload = Mathf.Min(extraBullets, desiredBulletsToReload);
        currentBullets += actualBulletsToReload;
        extraBullets -= actualBulletsToReload;
    }

    public bool CanReload()
    {
        return (extraBullets > 0 && currentBullets < gunData.magazineSize);
    }


}