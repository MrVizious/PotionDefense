using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModifier : ProjectileModifier
{
    public override void OnHit(Enemy enemy)
    {
        base.OnHit(enemy);
        enemy.gameObject.GetComponent<FireEffect>()?.End();
        FireEffect fireEffect = enemy.gameObject.AddComponent<FireEffect>();
        fireEffect.durationInSeconds = 7f;
        fireEffect.Begin();
    }
}