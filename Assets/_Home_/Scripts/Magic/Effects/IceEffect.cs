using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IceEffect : Effect
{
    public override void Begin(TowerData newData)
    {
        base.Begin(newData);
        if (enemy == null) return;
        enemy.speedModifier = data.effectSpeedModifier;
    }

    public override void End()
    {
        IceEffect[] iceEffects = GetComponents<IceEffect>();
        if (iceEffects.Length > 1)
        {
            foreach (IceEffect iceEffect in iceEffects)
            {
                if (iceEffect != this)
                {
                    enemy.speedModifier = iceEffect.data.effectSpeedModifier;
                }
            }
        }
        else if (iceEffects.Length == 1)
        {
            if (iceEffects[0] != this)
            {
                enemy.speedModifier = iceEffects[0].data.effectSpeedModifier;
            }
            else enemy.speedModifier = 1f;
        }
        else
        {
            enemy.speedModifier = 1f;
        }
        base.End();
    }
}
