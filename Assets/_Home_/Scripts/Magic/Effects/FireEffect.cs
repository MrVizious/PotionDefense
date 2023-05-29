using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FireEffect : Effect
{
    private float lastDamagedTickTime = 0;

    public override void Begin(TowerData data)
    {
        base.Begin(data);
        enemy?.Damage(data.effectDamageModifier);
        lastDamagedTickTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= lastDamagedTickTime + data.effectChance)
        {
            lastDamagedTickTime += data.effectChance;
            enemy?.Damage(data.effectChance);
        }
    }

}
