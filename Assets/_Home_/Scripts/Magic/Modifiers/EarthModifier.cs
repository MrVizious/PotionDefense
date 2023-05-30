using System.Collections;
using System.Collections.Generic;
using TypeReferences;
using UnityEngine;

public class EarthModifier : ProjectileModifier
{

    protected override TypeReference effectType => typeof(EarthEffect);

    // Destroy projectile if it is an enemy projectile and the probability is surpassed
    public override void OnHit(Enemy enemy)
    {
        if (enemy == null) return;
        if (!enemy.gameObject.activeInHierarchy) return;
        if (effectType == null) return;
        if (enemy.GetComponent(effectType) != null) return;

        ((Effect)enemy.gameObject.AddComponent(effectType)).Begin(data);
    }
}