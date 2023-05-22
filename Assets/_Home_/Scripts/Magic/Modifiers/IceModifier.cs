using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceModifier : ProjectileModifier
{
    public override void OnAcquire()
    {
        base.OnAcquire();
        projectile.speedModifier = 0.5f;
    }
    public override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);

        // Stops any previous slowing effects to the enemy
        enemy.gameObject.GetComponent<IceEffect>()?.End();
        // Adds the new slowing effect
        IceEffect slowEffect = enemy.gameObject.AddComponent<IceEffect>();
        slowEffect.speedModifier = 0.2f;
        slowEffect.durationInSeconds = 7f;
        slowEffect.Begin();
    }
}