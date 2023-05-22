using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FireEffect : Effect
{
    public float damagePerTick = 2f;
    public float secondsPerTick = 0.5f;
    private float lastDamagedTickTime = 0;

    public override void Begin()
    {
        base.Begin();
        enemy?.Damage(damagePerTick);
        lastDamagedTickTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= lastDamagedTickTime + secondsPerTick)
        {
            lastDamagedTickTime += secondsPerTick;
            enemy?.Damage(damagePerTick);
        }
    }

}
