using System.Collections;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;

public class ElectricityModifier : ProjectileModifier
{
    protected override TypeReference effectType => typeof(ElectricityEffect);

    public override void OnAcquire()
    {
        if (!projectile.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerProjectiles")))
        {
            Destroy(this);
        }
        base.OnAcquire();
    }
    public override void OnHit(Enemy enemy)
    {
        if (enemy == null) return;
        ElectricityEffect currentElectricityEffect = enemy.GetComponent<ElectricityEffect>();
        if (currentElectricityEffect == null)
        {
            enemy.gameObject.AddComponent<ElectricityEffect>().Begin(data);
        }
    }
}