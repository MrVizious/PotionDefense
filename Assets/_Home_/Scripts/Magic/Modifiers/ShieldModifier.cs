using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModifier : ProjectileModifier
{

    private float chanceToDisappear => data.effectChance;
    // Destroy projectile if it is an enemy projectile and the probability is surpassed
    public override void OnAcquire()
    {
        Debug.Log("Acquiring shield modifier");
        if (gameObject.layer != LayerMask.NameToLayer("EnemiesProjectiles"))
        {
            Destroy(this);
            return;
        }
        Debug.Log("Applying shield modifier");
        base.OnAcquire();

        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= chanceToDisappear) projectile.Release();
        Destroy(this);
    }
}