using System.Collections;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;

public class FireModifier : ProjectileModifier
{
    protected override TypeReference effectType => typeof(FireEffect);
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
        if (enemy == null) return;
        IceEffect currentIceEffect = enemy.GetComponent<IceEffect>();
        if (currentIceEffect == null)
        {
            base.OnHit(enemy);
        }
        else
        {
            Destroy(currentIceEffect);
        }
    }
}