using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowModifier : ProjectileModifier
{
    public override void OnHit(Enemy enemy)
    {
        enemy.gameObject.GetComponent<SlowEffect>()?.End();
        SlowEffect slowEffect = enemy.gameObject.AddComponent<SlowEffect>();
        //TODO: Remove line, just for testing
        slowEffect.speedModifier = 0.2f;
        slowEffect.durationInSeconds = 7f;
        slowEffect.Begin();
    }
}