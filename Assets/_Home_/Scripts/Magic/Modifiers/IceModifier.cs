using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceModifier : ProjectileModifier
{

    public override void OnAcquire()
    {
        if (!projectile.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerProjectiles")))
        {
            Destroy(this);
        }
        base.OnAcquire();
        projectile.speedModifier = data.effectSpeedModifier;
    }
    public override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);

        // Stops any previous slowing effects to the enemy
        enemy.gameObject.GetComponent<IceEffect>()?.End();
        // Adds the new slowing effect
        IceEffect slowEffect = enemy.gameObject.AddComponent<IceEffect>();
        slowEffect.speedModifier = data.effectSpeedModifier;
        slowEffect.durationInSeconds = data.effectDurationInSeconds;
        slowEffect.Begin();
    }

    public override void End()
    {
        projectile.speedModifier = 1f;
        base.End();
    }
}