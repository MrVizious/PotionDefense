using System.Collections;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;

public class IceModifier : ProjectileModifier
{
    protected override TypeReference effectType => typeof(IceEffect);

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
        if (enemy == null) return;
        FireEffect currentFireEffect = enemy.GetComponent<FireEffect>();
        if (currentFireEffect == null)
        {
            base.OnHit(enemy);
        }
        else
        {
            Destroy(currentFireEffect);
        }
    }

    public override void End()
    {
        projectile.speedModifier = 1f;
        base.End();
    }
}