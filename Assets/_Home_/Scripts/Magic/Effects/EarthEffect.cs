using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EarthEffect : Effect
{
    public override void Begin(TowerData newData)
    {
        base.Begin(newData);
        DamageAround();
    }

    private void DamageAround()
    {
        Collider2D[] collidersAround = Physics2D.OverlapCircleAll(transform.position, data.range);
        foreach (Collider2D collider in collidersAround)
        {
            Enemy checkedEnemy = collider.GetComponent<Enemy>();
            if (checkedEnemy == null) continue;
            checkedEnemy.Damage(data.effectDamageModifier);
        }
    }
}
