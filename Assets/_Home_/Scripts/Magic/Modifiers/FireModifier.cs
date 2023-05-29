using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModifier : ProjectileModifier
{
    public override void OnAcquire()
    {
        base.OnAcquire();
        if (!projectile.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerProjectiles")))
        {
            Destroy(this);
        }
    }
    public override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);
        enemy.gameObject.GetComponent<FireEffect>()?.End();
        FireEffect fireEffect = enemy.gameObject.AddComponent<FireEffect>();
        fireEffect.damagePerTick = data.effectDamageModifier;
        fireEffect.secondsPerTick = data.effectChance;
        fireEffect.durationInSeconds = data.effectDurationInSeconds;
        fireEffect.Begin();
    }
}