using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SlowEffect : Effect
{
    public float speedModifier = 0.75f;
    [Button]
    public override void Begin()
    {
        base.Begin();
        if (enemy == null) return;
        enemy.speedModifier = speedModifier;
    }

    [Button]
    public override void End()
    {
        enemy.speedModifier = 1f;
        base.End();
    }
}
