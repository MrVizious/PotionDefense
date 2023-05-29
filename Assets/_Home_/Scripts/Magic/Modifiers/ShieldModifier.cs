using System.Collections;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;

public class ShieldModifier : ProjectileModifier
{

    protected override TypeReference effectType => null;
    private float chanceToDisappear => data.effectChance;

    // Destroy projectile if it is an enemy projectile and the probability is surpassed
    public override void OnAcquire()
    {
        if (gameObject.layer != LayerMask.NameToLayer("EnemiesProjectiles"))
        {
            Destroy(this);
            return;
        }
        base.OnAcquire();

        float randomChance = Random.Range(0f, 1f);
        if (randomChance <= chanceToDisappear) projectile.Release();
        Destroy(this);
    }
}